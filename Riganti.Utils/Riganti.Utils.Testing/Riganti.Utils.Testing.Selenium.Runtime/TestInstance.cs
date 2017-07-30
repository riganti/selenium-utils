using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public class TestInstance
    {
        private readonly SeleniumTestsConfiguration configuration;
        private readonly TestSuiteRunner runner;
        private readonly TestConfiguration testConfiguration;
        private readonly Action<BrowserWrapper> testAction;


        public TestInstance(SeleniumTestsConfiguration configuration, TestSuiteRunner runner, TestConfiguration testConfiguration, Action<BrowserWrapper> testAction)
        {
            this.configuration = configuration;
            this.runner = runner;
            this.testConfiguration = testConfiguration;
            this.testAction = testAction;
        }

        public async Task RunAsync()
        {
            await RetryAsync(RunAsyncCore, configuration.TestRunOptions.TestAttemptsCount);
        }

        private async Task RunAsyncCore()
        {
            IWebBrowser browser = null;
            try
            {
                browser = await runner.WebBrowserPool.GetOrCreateBrowser(testConfiguration.Factory);

                RunTest(browser);
            }
            finally
            {
                if (browser != null)
                {
                    runner.WebBrowserPool.ReturnBrowserToPool(browser);
                }
            }
        }

        private void RunTest(IWebBrowser browser)
        {
            // create a new browser wrapper
            var wrapper = new BrowserWrapper(browser);

            // prepare test context
            var testContext = runner.TestContextProvider.CreateTestContext(testConfiguration);
            using (testConfiguration.Factory.TestContextAccessor.Scope(testContext))
            {
                try
                {
                    // run actual test
                    testAction(wrapper);
                    browser.ClearDriverState();
                }
                catch
                {
                    // recreate the browser
                    browser.RecreateDriver();
                    throw;
                }
            }
        }



        private static async Task RetryAsync(Func<Task> action, int testAttemptsCount)
        {
            var errors = new List<Exception>();
            for (int i = 0; i < testAttemptsCount; i++)
            {
                try
                {
                    await action();
                    return;
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }
            throw new AggregateException(errors);
        }
    }
}