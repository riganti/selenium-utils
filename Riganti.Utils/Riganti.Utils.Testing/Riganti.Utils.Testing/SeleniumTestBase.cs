using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class SeleniumTestBase : ITestBase
    {

        public TestContext TestContext { get; set; }
        private WebDriverFacotryRegistry factory;
        private string screenshotsFolderPath;
        private string CurrentSubSection { get; set; }
        private Type ExpectedExceptionType { get; set; }
        protected IWebDriver LatestLiveWebDriver { get; set; }
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

        public WebDriverFacotryRegistry Factory => WebDriverFacotryRegistry.Default.Clone();

        protected virtual List<IWebDriverFactory> BrowserFactories => Factory.BrowserFactories;
        private BrowserWrapper helper;

        /// <summary>
        /// Runs the specified action in all configured browsers.
        /// </summary>
        protected virtual void RunInAllBrowsers(Action<BrowserWrapper> action)
        {
            CheckAvailableWebDriverFactories();
            foreach (var browserFactory in BrowserFactories)
            {

                string browserName;
                Exception exception;

                TryExecuteTest(action, browserFactory, out browserName, out exception);

                if (exception != null)
                {
                    if (CurrentSubSection == null)
                        throw new SeleniumTestFailedException(exception, browserName, ScreenshotsFolderPath);
                    throw new SeleniumTestFailedException(exception, browserName, ScreenshotsFolderPath, CurrentSubSection);
                }
            }
        }

        private void TryExecuteTest(Action<BrowserWrapper> action, IWebDriverFactory browserFactory, out string browserName, out Exception exception)
        {
            var attemptNumber = 0;
            do
            {
                attemptNumber++;
                exception = null;
                using (var browser = browserFactory.CreateNewInstance())
                {
                    helper = new BrowserWrapper(browser.Driver, this);
                    browserName = browser.GetType().Name;

                    WriteLine($"Testing browser '{browserName}' attempt no. {attemptNumber}");

                    try
                    {
                        action(helper);
                    }
                    catch (Exception ex)
                    {
                        bool isExpected = false;
                        if (ExpectedExceptionType != null)
                        {
                            isExpected = ex.GetType() == ExpectedExceptionType || (AllowDerivedExceptionTypes && ExpectedExceptionType.IsInstanceOfType(ex));
                        }


                        if (!isExpected)
                        {
                            TakeScreenshot(attemptNumber, helper);
                            // fail the test
                            exception = ex;
                        }
                    }
                }
            }
            while (exception != null && attemptNumber == SeleniumTestsConfiguration.TestAttemps);
        }

        private void CheckAvailableWebDriverFactories()
        {
            if (BrowserFactories.Count == 0)
            {
                throw new Exception("Factory doesn't contains drivers! Enable one driver at least to start UI Tests!");
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





        protected virtual void TakeScreenshot(int attemptNumber, BrowserWrapper browserWrapper)
        {  // make screenshot
            try
            {

                LogCurrentlyPerformedAction("Taking screenshot");

                var filename = Path.Combine(ScreenshotsFolderPath, $"{TestContext.FullyQualifiedTestClassName}_{TestContext.TestName}" + attemptNumber + ".png");
                browserWrapper.TakeScreenshot(filename);
                TestContext.AddResultFile(filename);

            }
            catch
            {
                //ignore
            }
        }

        protected virtual void WriteLine(string message)
        {
            TestContext?.WriteLine(message);
            if (Debugger.IsAttached)
            {
                Debugger.Log(1, Debugger.DefaultCategory, message);
            }

        }
        public virtual void Log(string message)
        {
            WriteLine(message);
        }

        public virtual void Log(Exception exception)
        {
            WriteLine(exception.ToString());
        }

        protected virtual void LogCurrentlyPerformedAction(string actionName)
        {
            WriteLine($"Currently performing: {actionName}");
        }

        protected void ExpectException(Type type, bool allowDerivedTypes = false)
        {
            AllowDerivedExceptionTypes = allowDerivedTypes;
            ExpectedExceptionType = type;
        }

        public bool AllowDerivedExceptionTypes { get; set; }

        [TestCleanup]
        public virtual void CleanUp()
        {
            LatestLiveWebDriver?.Dispose();
        }
    }
}