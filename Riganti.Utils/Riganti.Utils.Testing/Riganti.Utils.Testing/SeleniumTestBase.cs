using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class SeleniumTestBase
    {
        public TestContext TestContext { get; set; }
        private WebDriverFacotryRegistry factory;
        private string screenshotsFolderPath;

        public string ScreenshotsFolderPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(screenshotsFolderPath))
                {
                    screenshotsFolderPath = TestContext.TestDeploymentDir;
                }
                return screenshotsFolderPath;
            }
            set { screenshotsFolderPath = value; }
        }

        public WebDriverFacotryRegistry Factory => factory ?? (factory = new WebDriverFacotryRegistry());

        protected virtual List<IWebDriverFactory> BrowserFactories => Factory.BrowserFactories;

        /// <summary>
        /// Runs the specified action in all configured browsers.
        /// </summary>
        protected virtual void RunInAllBrowsers(Action<BrowserWrapper> action)
        {
            if (BrowserFactories.Count == 0)
            {
                throw new Exception("Factory doesn't contains drivers! Enable one driver at least to start UI Tests!");
            }
            foreach (var browserFactory in BrowserFactories)
            {
                var attemptNumber = 0;
                string browserName;
                Exception exception;
                do
                {
                    attemptNumber++;
                    exception = null;
                    var browser = browserFactory.CreateNewInstance();
                    browserName = browser.GetType().Name;
                    var helper = new BrowserWrapper(browser);

                    try
                    {
                        action(helper);
                    }
                    catch (Exception ex)
                    {
                        // make screenshot
                        try
                        {
                            TakeScreenshot(attemptNumber, helper);
                        }
                        catch
                        {
                        }

                        // fail the test
                        exception = ex;
                    }
                    finally
                    {
                        helper.Dispose();
                    }
                }
                while (exception != null && attemptNumber == SeleniumTestsConfiguration.TestAttemps);

                if (exception != null)
                {
                    throw new SelenumTestFailedException(exception, browserName, ScreenshotsFolderPath);
                }
            }
        }

        protected virtual void TakeScreenshot(int attemptNumber, BrowserWrapper helper)
        {
            var filename = Path.Combine(ScreenshotsFolderPath, $"{TestContext.FullyQualifiedTestClassName}_{TestContext.TestName}" + attemptNumber + ".png");
            helper.TakeScreenshot(filename);
            TestContext.AddResultFile(filename);
        }
    }
}