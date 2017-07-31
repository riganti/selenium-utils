using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public class TestInstance
    {
        private readonly TestSuiteRunner runner;
        private readonly TestConfiguration testConfiguration;
        private readonly string testName;
        private readonly Action<BrowserWrapper> testAction;
        private int testAttemptNumber;


        public TestInstance(TestSuiteRunner runner, TestConfiguration testConfiguration, string testName, Action<BrowserWrapper> testAction)
        {
            this.runner = runner;
            this.testConfiguration = testConfiguration;
            this.testName = testName;
            this.testAction = testAction;
        }

        public async Task RunAsync()
        {
            await RetryAsync(RunAsyncCore, runner.Configuration.TestRunOptions.TestAttemptsCount);
        }

        private async Task RunAsyncCore()
        {
            IWebBrowser browser = null;
            try
            {
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Acquiring browser from browser pool");
                browser = await runner.WebBrowserPool.GetOrCreateBrowser(testConfiguration.Factory);

                RunTest(browser);

                await runner.WebBrowserPool.ReturnBrowserToPool(browser);
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Browser {browser.UniqueName} returned to browser pool");
            }
            catch (Exception ex)
            {
                await runner.WebBrowserPool.DisposeBrowser(browser);
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Test failed {ex}");
                throw;
            }
        }

        private void RunTest(IWebBrowser browser)
        {
            // create a new browser wrapper
            var wrapper = new BrowserWrapper(browser);

            // prepare test context
            var testContext = runner.TestContextProvider.CreateTestContext(testConfiguration);
            using (testConfiguration.Factory.TestSuiteRunner.TestContextAccessor.Scope(testContext))
            {
                try
                {
                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Running test in browser instance {browser.UniqueName}");

                    // run actual test
                    testAction(wrapper);

                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Test passed");
                }
                catch
                {
                    TakeScreenshot(wrapper);

                    // recreate the browser
                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Test failed");
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
                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Test attempt #{testAttemptNumber} failed.");
                    errors.Add(ex);
                }
            }
            throw new AggregateException(errors);
        }

        private void TakeScreenshot(BrowserWrapper browserWrapper)
        {
            var testContext = runner.TestContextAccessor.GetTestContext();

            try
            {
                var filename = Path.Combine(testContext.DeploymentDirectory, $"{testContext.FullyQualifiedTestClassName}_{testContext.TestName}_{testAttemptNumber}.png");
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Taking screenshot {filename}");

                browserWrapper.TakeScreenshot(filename);
                runner.TestContextAccessor.GetTestContext().AddResultFile(filename);
            }
            catch (Exception ex)
            {
                runner.LogError(new Exception($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Failed to take screenshot.", ex));
            }
        }
    }
}