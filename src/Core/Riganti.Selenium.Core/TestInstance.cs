using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Factories;
using OpenQA.Selenium;

namespace Riganti.Selenium.Core
{
    public class TestInstance : ITestInstance
    {
        private readonly Action<IBrowserWrapper> testAction;
        private int testAttemptNumber;

        public TestSuiteRunner TestSuiteRunner { get; }
        public TestConfiguration TestConfiguration { get; }
        public string TestName { get; }
        public ISeleniumTest TestClass { get; }
        public IWebBrowser CurrentWebBrowser { get; private set; }


        public TestInstance(TestSuiteRunner runner, ISeleniumTest testClass, TestConfiguration testConfiguration,
            string testName, Action<IBrowserWrapper> testAction)
        {
            this.TestSuiteRunner = runner;
            this.TestClass = testClass;
            this.TestConfiguration = testConfiguration;
            this.TestName = testName;
            this.testAction = testAction;
        }

        public async Task RunAsync()
        {
            await RetryAsync(RunAsyncCore, TestSuiteRunner.Configuration.TestRunOptions.TestAttemptsCount);
        }

        private async Task RunAsyncCore()
        {
            try
            {
                TestSuiteRunner.LogVerbose(
                    $"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Acquiring browser from browser pool");
                CurrentWebBrowser = await TestSuiteRunner.WebBrowserPool.GetOrCreateBrowser(TestConfiguration.Factory);

                RunTest(CurrentWebBrowser);

                await TestSuiteRunner.WebBrowserPool.ReturnBrowserToPool(CurrentWebBrowser);
                TestSuiteRunner.LogVerbose(
                    $"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Browser {CurrentWebBrowser.UniqueName} returned to browser pool");
                CurrentWebBrowser = null;
            }
            catch (Exception ex)
            {
                await RunCrashed(ex);
                throw;
            }
        }

        private void AddExceptionMetadata(TestExceptionBase exception, IBrowserWrapper wrapper)
        {
            ExecuteSafe(() => { exception.WebBrowser = CurrentWebBrowser.Factory.Name; });

            var result = ExecuteSafe(() => { exception.CurrentUrl = CurrentWebBrowser.Driver.Url; });
            
            // when browser is frozen then taking screenshot is non-sense.
            if (!result) return;

            ExecuteSafe(() => { exception.Screenshot = TakeScreenshot(wrapper); });
        }

        private bool ExecuteSafe(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch(UnhandledAlertException ex) //when alert is still open and test is done
            {
                try
                {
                    CurrentWebBrowser.Driver.SwitchTo().Alert().Dismiss();
                }
                catch
                {
                    return false;
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task RunCrashed(Exception ex)
        {
            await TestSuiteRunner.WebBrowserPool.DisposeBrowser(CurrentWebBrowser);
            TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test failed {ex}");
            CurrentWebBrowser = null;
        }

        private void RunTest(IWebBrowser browser)
        {
            // create a new browser wrapper
            var wrapper = TestSuiteRunner.ServiceFactory.Resolve<IBrowserWrapper>(browser, browser.Driver, this, new ScopeOptions());

            // prepare test context
            var testContext = TestSuiteRunner.TestContextProvider.CreateTestContext(this);
            using (TestConfiguration.Factory.TestSuiteRunner.TestContextAccessor.Scope(testContext))
            {
                try
                {
                    TestSuiteRunner.LogVerbose(
                        $"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Running test in browser instance {browser.UniqueName}");

                    // run actual test
                    testAction(wrapper);

                    TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test passed");
                }
                catch (TestExceptionBase ex)
                {
                    AddExceptionMetadata(ex, wrapper);
                    throw;
                }
                catch
                {
                    // recreate the browser
                    TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test failed");
                    throw;
                }
            }
        }

        private async Task RetryAsync(Func<Task> action, int testAttemptsCount)
        {
            var errors = new List<Exception>();
            for (testAttemptNumber = 1; testAttemptNumber <= testAttemptsCount; testAttemptNumber++)
            {
                try
                {
                    await action();
                    return;
                }
                catch (Exception ex)
                {
                    TestSuiteRunner.LogVerbose(
                        $"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test attempt #{testAttemptNumber} failed.");
                    errors.Add(ex);
                }
            }

            throw new SeleniumTestFailedException(errors);
        }

        private string TakeScreenshot(IBrowserWrapper browserWrapper)
        {
            var testContext = TestSuiteRunner.TestContextProvider.GetGlobalScopeTestContext();
            try
            {
                var deploymentDirectory = testContext.DeploymentDirectory;
                if (!string.IsNullOrWhiteSpace(TestSuiteRunner.Configuration.TestRunOptions.ScreenshotPath))
                {
                    deploymentDirectory = CreateDirectory(Path.Combine(TestSuiteRunner.Configuration.TestRunOptions.ScreenshotPath, DateTime.UtcNow.ToString("yyyyMMdd")));
                }

                var filename = Path.Combine(deploymentDirectory,
                    $"{testContext.FullyQualifiedTestClassName}_{testContext.TestName}_{testAttemptNumber}.png");
                TestSuiteRunner.LogVerbose(
                    $"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Taking screenshot {filename}");
                browserWrapper.TakeScreenshot(filename);
                TestSuiteRunner.TestContextAccessor.GetTestContext().AddResultFile(filename);
                return filename;
            }
            catch (Exception ex)
            {
                TestSuiteRunner.LogError(new Exception(
                    $"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Failed to take screenshot.", ex));
                return null;
            }
        }

        private string CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}