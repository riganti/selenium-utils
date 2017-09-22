using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core
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


        public TestInstance(TestSuiteRunner runner, ISeleniumTest testClass, TestConfiguration testConfiguration, string testName, Action<IBrowserWrapper> testAction) 
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
                TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Acquiring browser from browser pool");
                CurrentWebBrowser = await TestSuiteRunner.WebBrowserPool.GetOrCreateBrowser(TestConfiguration.Factory);

                RunTest(CurrentWebBrowser);

                await TestSuiteRunner.WebBrowserPool.ReturnBrowserToPool(CurrentWebBrowser);
                TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Browser {CurrentWebBrowser.UniqueName} returned to browser pool");
                CurrentWebBrowser = null;
            }
            catch (Exception ex)
            {
                await TestSuiteRunner.WebBrowserPool.DisposeBrowser(CurrentWebBrowser);
                TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test failed {ex}");
                CurrentWebBrowser = null;

                throw;
            }
        }

        private void RunTest(IWebBrowser browser)
        {
            // create a new browser wrapper
            var wrapper =  new BrowserWrapper(browser, browser.Driver, this, new ScopeOptions());

            // prepare test context
            var testContext = TestSuiteRunner.TestContextProvider.CreateTestContext(this);
            using (TestConfiguration.Factory.TestSuiteRunner.TestContextAccessor.Scope(testContext))
            {
                try
                {
                    TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Running test in browser instance {browser.UniqueName}");

                    // run actual test
                    testAction(wrapper);

                    TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test passed");
                }
                catch
                {
                    TakeScreenshot(wrapper);

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
                    TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Test attempt #{testAttemptNumber} failed.");
                    errors.Add(ex);
                }
            }
            throw new AggregateException(errors);
        }

        private void TakeScreenshot(IBrowserWrapper browserWrapper) 
        {

            
            throw new NotImplementedException();
            //var testContext = TestSuiteRunner.TestContextAccessor.GetTestContext();

            //try
            //{
            //    var filename = Path.Combine(testContext.DeploymentDirectory, $"{testContext.FullyQualifiedTestClassName}_{testContext.TestName}_{testAttemptNumber}.png");
            //    TestSuiteRunner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Taking screenshot {filename}");
            //    browserWrapper.TakeScreenshot(filename);
            //    TestSuiteRunner.TestContextAccessor.GetTestContext().AddResultFile(filename);
            //}
            //catch (Exception ex)
            //{
            //    TestSuiteRunner.LogError(new Exception($"(#{Thread.CurrentThread.ManagedThreadId}) {TestName}: Failed to take screenshot.", ex));
            //}
        }
    }
}