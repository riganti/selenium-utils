using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// This class represents the funcation of UI tests based on selenium. Provides logging, re-try logic, screenshots, etc.. 
    /// </summary>
    public abstract class SeleniumTestBase : ITestBase
    {
        public static readonly FastModeWebDriverFactoryRegistry FastModeFactoryRegistry;
        private static int testsIndexer = 0;
        public Guid ActiveScope { get; set; } = Guid.Empty;
        static SeleniumTestBase()
        {
            FastModeFactoryRegistry = new FastModeWebDriverFactoryRegistry();
            Loggers = new List<ILogger>();
            if (SeleniumTestsConfiguration.StandardOutputLogger) Loggers.Add(new StandardOutputLogger());
            if (SeleniumTestsConfiguration.TeamcityLogger) Loggers.Add(new TeamcityLogger());
            if (SeleniumTestsConfiguration.DebuggerLogger) Loggers.Add(new DebuggerLogger());
            if (SeleniumTestsConfiguration.DebugLogger) Loggers.Add(new DebugLogger());

            //default logger
            if (Loggers.Count == 0 && !SeleniumTestsConfiguration.DebugLoggerContainedKey)
            {
                Loggers.Add(new DebugLogger());
            }
        }

        public static List<ILogger> Loggers { get; protected set; }

        /// <summary>
        ///  Used to store information that is provided to tests. (MSTest)
        /// </summary>
        public virtual ITestContext TestContext { get; set; }

        private WebDriverFactoryRegistry factory;
        private string screenshotsFolderPath;
        private string CurrentSubSection { get; set; }
        private Type ExpectedExceptionType { get; set; }
        /// <summary>
        /// Latest live driver instance. 
        /// </summary>
        protected IWebDriver LatestLiveWebDriver { get; set; }
        /// <summary>
        /// Path to screenshot storage in file system.
        /// </summary>
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
        /// <summary>
        /// Provides re-try logic, logging mechanism, screenshot storing and other basic functionality of Selenium tests.  
        /// </summary>
        public SeleniumTestBase()
        {
            if (SeleniumTestsConfiguration.TestContextLogger)
            {
                var logger = Loggers.FirstOrDefault(s => s is TestContextLogger);
                Loggers.Remove(logger);
                Loggers.Add(new TestContextLogger(this));
            }
        }

        public WebDriverFactoryRegistry FactoryRegistry => factory ?? (factory = new WebDriverFactoryRegistry());

        protected virtual List<IWebDriverFactory> BrowserFactories => SeleniumTestsConfiguration.FastMode ? FastModeFactoryRegistry.BrowserFactories : FactoryRegistry.BrowserFactories;
        private BrowserWrapper wrapper;

        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        protected virtual void RunInAllBrowsers(Action<BrowserWrapper> testBody)
        {
            Log("Test no.: " + testsIndexer++, 10);
            CheckAvailableWebDriverFactories();
            foreach (var browserFactory in BrowserFactories)
            {
                string browserName;
                Exception exception = null;

                CurrentTestExceptions.Clear();
                if (!(SeleniumTestsConfiguration.PlainMode || SeleniumTestsConfiguration.DeveloperMode))
                {
                    TryExecuteTest(testBody, browserFactory, out browserName, out exception);
                }
                else
                {
                    //developer mode - it throws exception directly without catch statement
                    ExecuteTest(testBody, browserFactory, out browserName);
                }
                if (exception != null)
                {
                    Exception throwException;
                    if (CurrentSubSection == null)
                    {
                        throwException = new SeleniumTestFailedException(CurrentTestExceptions, browserName, ScreenshotsFolderPath);
                    }
                    else
                    {
                        throwException = new SeleniumTestFailedException(CurrentTestExceptions, browserName, ScreenshotsFolderPath, CurrentSubSection);
                    }

                    Log("\r\n\r\n\r\n\r\nException logging: \r\n\r\n");
                    Log(throwException);
                    throw throwException;
                }
            }
        }

        /// <summary>
        /// Executes test withnout caching exceptions
        /// </summary>
        /// <param name="testBody">Test to execute</param>
        /// <param name="browserFactory"></param>
        /// <param name="browserName"></param>
        /// <remarks>The ExpectException is ignored.</remarks>
        protected virtual void ExecuteTest(Action<BrowserWrapper> testBody, IWebDriverFactory browserFactory, out string browserName)
        {
            try
            {
                var browser = LatestLiveWebDriver = browserFactory.CreateNewInstance();
                var scope = new ScopeOptions() { CurrentWindowHandle = browser.CurrentWindowHandle };
                wrapper = new BrowserWrapper(browser, this, scope);
                browserName = browser.GetType().Name;
                testBody(wrapper);
            }
            finally
            {
                DisposeBrowsers(browserFactory);
            }
        }

        protected List<Exception> CurrentTestExceptions = new List<Exception>();
        /// <summary>
        /// Executes test in specified browser with re-try logic, logging and screenshots in case of failure.
        /// </summary>
        /// <param name="testBody">Test to execute.</param>
        /// <param name="browserFactory">Factory of specific browser.</param>
        /// <param name="browserName">Name of the browser.</param>
        /// <param name="exception">Exception with details of failure.</param>
        protected virtual void TryExecuteTest(Action<BrowserWrapper> testBody, IWebDriverFactory browserFactory, out string browserName, out Exception exception)
        {
            var attemptNumber = 0;
            var attampsMaximum = SeleniumTestsConfiguration.TestAttempts + (SeleniumTestsConfiguration.FastMode ? 1 : 0);
            do
            {
                attemptNumber++;
                WriteLine($"Attamp #{attemptNumber} starts....");
                exception = null;
                bool exceptionWasThrow = false;

                var browser = LatestLiveWebDriver = browserFactory.CreateNewInstance();
                LogDriverId(browser, "Driver creation - TryExecuteTest");
                var scope = new ScopeOptions() { CurrentWindowHandle = browser.CurrentWindowHandle };
                wrapper = new BrowserWrapper(browser, this, scope);
                browserName = browser.GetType().Name;

                WriteLine($"Testing browser '{browserName}' attempt no. {attemptNumber}");

                try
                {
                    BeforeSpecificBrowserTestStarts(wrapper);
                    Log("Execution of user test starting.", 10);
                    testBody(wrapper);
                    Log("Execution of user test ended.", 10);
                    AfterSpecificBrowserTestEnds(wrapper);
                }
                catch (Exception ex)
                {
                    exceptionWasThrow = true;
                    bool isExpected = false;
                    if (ExpectedExceptionType != null)
                    {
                        isExpected = ex.GetType() == ExpectedExceptionType || (AllowDerivedExceptionTypes && ExpectedExceptionType.IsInstanceOfType(ex));
                    }
                    Log("Test is expected to drop: " + isExpected, 10);
                    if (!isExpected)
                    {
                        TakeScreenshot(attemptNumber, wrapper);
                        // fail the test
                        CurrentTestExceptions.Add(exception = ex);
                        Log("Test attemp was not successfull! - TEST ATTAMP FAILED", 10);
                    }
                    if (attemptNumber < attampsMaximum)
                    {
                        RecreateBrowsers(browserFactory);
                    }
                    else
                    {
                        DisposeBrowsers(browserFactory);
                    }
                }
                finally
                {
                    if (!exceptionWasThrow)
                        CleanBrowsers(browserFactory);
                }
                if (ExpectedExceptionType != null && !exceptionWasThrow)
                {
                    CurrentTestExceptions.Add(exception = new SeleniumTestFailedException("Test was supposted to fail and it did not."));
                }
            }
            while (exception != null && attemptNumber < attampsMaximum);
        }

        private void CleanBrowsers(IWebDriverFactory browserFactory)
        {
            if (browserFactory is IFastModeFactory)
            {
                Log("TryExecuteTest: Cleaning browser", 10);
                ((IFastModeFactory)browserFactory).Clear();
            }
            else
            {
                Log("TryExecuteTest: Cleaning browser", 10);
                wrapper.Dispose();
            }
        }

        private void RecreateBrowsers(IWebDriverFactory browserFactory)
        {
            if (browserFactory is IFastModeFactory)
            {
                Log("TryExecuteTest: Recreating", 10);
                ((IFastModeFactory)browserFactory).Recreate();
            }
            else
            {
                wrapper.Dispose();
            }
        }

        private void DisposeBrowsers(IWebDriverFactory browserFactory)
        {
            if (browserFactory is IFastModeFactory)
            {
                Log("TryExecuteTest: Disposing", 10);
                ((IFastModeFactory)browserFactory).Dispose();
            }
            else
            {
                wrapper?.Dispose();
            }
        }

        protected virtual void CheckAvailableWebDriverFactories()
        {
            if (BrowserFactories.Count == 0)
            {
                throw new Exception("Factory doesn't contains drivers! Enable one driver at least to start UI Tests!");
            }
        }

        [Obsolete]
        public virtual void RunTestSubSection(string subSectionName, Action<BrowserWrapper> action)
        {
            Log($"Starts testing of section: {subSectionName}", 5);
            CurrentSubSection = subSectionName;
            action(wrapper);
            CurrentSubSection = null;
            Log($"Testing of section succesfully completed.", 5);
        }
        /// <summary>
        /// Takes screenshot by call to WebDriver.
        /// </summary>
        /// <param name="attemptNumber">Number of test iteration.</param>
        /// <param name="browserWrapper">Browser to take screenshot from.</param>
        protected virtual void TakeScreenshot(int attemptNumber, BrowserWrapper browserWrapper)
        {  // make screenshot
            try
            {
                LogCurrentlyPerformedAction("Taking screenshot");

                var filename = Path.Combine(ScreenshotsFolderPath, $"{TestContext.FullyQualifiedTestClassName}_{TestContext.TestName}" + attemptNumber + ".png");
                browserWrapper.TakeScreenshot(filename);
                LogCurrentlyPerformedAction($"Screenshot saved to: {filename}");
                TestContext.AddResultFile(filename);
            }
            catch (Exception ex)
            {
                Log($"Screenshot CANNOT be saved!", 10);
                Log(ex);
            }
        }

        protected static void WriteLine(string message)
        {
            Loggers.ForEach(logger =>
            {
                logger.WriteLine(message);
            });
        }

        /// <summary>
        /// Writes messages to registered loggers.
        /// </summary>
        /// <param name="message">message that is going to be written by logger</param>
        /// <param name="priority">Priority of message. 0 - the highest, 10 - the lowest = internal system log message level</param>
        public static void Log(string message, int priority = 0)
        {
            if (SeleniumTestsConfiguration.LoggingPriorityMaximum >= priority)
                WriteLine(message);
        }

        public virtual void Log(Exception exception)
        {
            WriteLine(exception.ToString());
        }
        /// <summary>
        /// Log driver id for better debugging output when some driver is stuck.
        /// </summary>
        /// <param name="browser">Browser to be logged.</param>
        /// <param name="checkpoint">Name of point where you are logging from.</param>
        public static void LogDriverId(IWebDriver browser, string checkpoint)
        {
            var driver = browser as IWebDriverWrapper;
            if (driver != null)
            {
                Log($"GUID: {driver.DriverId}      | Disposed: {driver.Disposed} | Checkpoint:{checkpoint}", 10);
            }
        }

        protected virtual void LogCurrentlyPerformedAction(string actionName)
        {
            Log($"Currently performing: {actionName}", 9);
        }
        /// <summary>
        /// This method registers type of exception which is expected to be thrown during <see cref="RunInAllBrowsers"/> method execution. Call this method before  <see cref="RunInAllBrowsers"/>.
        /// </summary>
        /// <param name="type">expected exception to be thrown</param>
        /// <param name="allowDerivedTypes"></param>
        /// <remarks>This functionality is not supported in Developer Mode!</remarks>
        protected void ExpectException(Type type, bool allowDerivedTypes = false)
        {
            AllowDerivedExceptionTypes = allowDerivedTypes;
            ExpectedExceptionType = type;
        }

        public bool AllowDerivedExceptionTypes { get; set; }

        public virtual void BeforeSpecificBrowserTestStarts(BrowserWrapper browser)
        {
        }

        public virtual void AfterSpecificBrowserTestEnds(BrowserWrapper browser)
        {
        }

        public virtual void TestCleanUp()
        {
            AllowDerivedExceptionTypes = false;
            ExpectedExceptionType = null;
            Log("Test cleanup.");
        }

        public virtual void TotalCleanUp()
        {
            Log("Class cleanup.");
            if (SeleniumTestsConfiguration.FastMode)
                FastModeFactoryRegistry.Dispose();
        }
    }
}