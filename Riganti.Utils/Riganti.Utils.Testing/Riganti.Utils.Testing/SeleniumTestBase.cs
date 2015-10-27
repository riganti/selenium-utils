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
        private string CurrentSubSection { get; set; }

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
        private BrowserWrapper helper;

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
                    helper = new BrowserWrapper(browser);
                    browserName = browser.GetType().Name;

                    WriteLine($"Testing browser '{browserName}' attempt no. {attemptNumber}");

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
                    if (CurrentSubSection == null)
                        throw new SelenumTestFailedException(exception, browserName, ScreenshotsFolderPath);
                    throw new SelenumTestFailedException(exception, browserName, ScreenshotsFolderPath, CurrentSubSection);
                }
            }
        }


        public virtual void RunTestSubSection(string subSectionName, Action<BrowserWrapper> action)
        {
            WriteLine($"Starts testing of section: {subSectionName}");
            CurrentSubSection = subSectionName;
            action(helper);
            CurrentSubSection = null;
            WriteLine($"Testing of section succesfully completed.");
        }





        protected virtual void TakeScreenshot(int attemptNumber, BrowserWrapper helper)
        {
            LogCurrentlyPerformedAction("Taking screenshot");

            var filename = Path.Combine(ScreenshotsFolderPath, $"{TestContext.FullyQualifiedTestClassName}_{TestContext.TestName}" + attemptNumber + ".png");
            helper.TakeScreenshot(filename);
            TestContext.AddResultFile(filename);
        }

        protected virtual void WriteLine(string message)
        {
            TestContext?.WriteLine(message);
        }

        protected virtual void LogCurrentlyPerformedAction(string actionName)
        {
            WriteLine($"Currently performing: {actionName}");
        }

    }
}