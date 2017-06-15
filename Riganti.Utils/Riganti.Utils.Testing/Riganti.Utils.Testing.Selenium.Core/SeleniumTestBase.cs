using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// This class represents the funcation of UI tests based on selenium. Provides logging, re-try logic, screenshots, etc..
    /// </summary>
    public abstract class SeleniumTestBase : ISeleniumTest
    {
        /// <summary>
        ///  Factory to create drivers that supports FastMode (cleaning and re-using the same browser for more tests)
        /// </summary>
        public static readonly FastModeWebDriverFactoryRegistry FastModeFactoryRegistry;

        private static int testsIndexer = 0;

        /// <summary>
        /// Unique ID of the scope/frame/window.
        /// </summary>
        public Guid CurrentScope { get; set; } = Guid.Empty;

        static SeleniumTestBase()
        {
            FastModeFactoryRegistry = new FastModeWebDriverFactoryRegistry();
            Loggers = new List<ILogger>();
            if (SeleniumTestsConfiguration.StandardOutputLogger) Loggers.Add(new StandardOutputLogger());
            if (SeleniumTestsConfiguration.TeamcityLogger) Loggers.Add(new TeamcityLogger());
            if (SeleniumTestsConfiguration.DebuggerLogger) Loggers.Add(new DebuggerLogger());
            if (SeleniumTestsConfiguration.DebugLogger) Loggers.Add(new DebugLogger());
            if (SeleniumTestsConfiguration.TraceLogger) Loggers.Add(new TraceLogger());
            //default logger
            if (Loggers.Count == 0 && !SeleniumTestsConfiguration.DebugLoggerContainedKey)
            {
                Loggers.Add(new DebugLogger());
            }
        }

        /// <summary>
        /// Collection of implementations of logging mechanisms.
        /// </summary>
        public static List<ILogger> Loggers { get; protected set; }

        /// <summary>
        ///  Used to store information that is provided to tests. (MSTest)
        /// </summary>
        public virtual ITestContext Context { get; set; }

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
                    screenshotsFolderPath = Context.TestDeploymentDir;
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

        /// <summary>
        /// Place to store browser factories, that does NOT support FastMode.
        /// </summary>
        public WebDriverFactoryRegistry FactoryRegistry => factory ?? (factory = new WebDriverFactoryRegistry());

        /// <summary>
        /// Place to store browser factories, that does NOT support FastMode.
        /// </summary>
        protected virtual List<IWebDriverFactory> BrowserFactories
            =>
            SeleniumTestsConfiguration.FastMode
                ? FastModeFactoryRegistry.BrowserFactories
                : FactoryRegistry.BrowserFactories;

        private BrowserWrapper wrapper;

        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        protected virtual void RunInAllBrowsers(Action<BrowserWrapper> testBody)
        {
            try
            {
                Log("Test no.: " + testsIndexer++, 10);
                CheckAvailableWebDriverFactories();
                foreach (var browserFactory in BrowserFactories)
                {
                    string browserName;
                    Exception exception = null;
                    string url = null;
                    CurrentTestExceptions.Clear();
                    if (!(SeleniumTestsConfiguration.PlainMode || SeleniumTestsConfiguration.DeveloperMode))
                    {
                        TryExecuteTest(testBody, browserFactory, out browserName, out exception, out url);
                    }
                    else
                    {
                        //developer mode - it throws exception directly without catch statement
                        ExecuteTest(testBody, browserFactory, out browserName);
                    }
                    if (exception != null)
                    {
                        var throwException = new SeleniumTestFailedException(CurrentTestExceptions, browserName)
                        {
                            ScreenshotPath =  ScreenshotsFolderPath,
                            CurrentSubSection = CurrentSubSection,
                             Url = url
                        };


                        Log("\r\n\r\n\r\n\r\nException logging: \r\n\r\n");
                        Log(throwException);
                        throw throwException;
                    }
                }
            }
            finally
            {
                TestCleanUp();
            }
        }

        /// <summary>
        /// Tries to return current url of the IWebDriver or null.
        /// </summary>
        protected virtual string TryGetBrowserCurrentUrl(IWebDriver driver)
        {
            try
            {
                return driver.Url;
            }
            catch
            {
                return null;
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
        protected virtual void TryExecuteTest(Action<BrowserWrapper> testBody, IWebDriverFactory browserFactory, out string browserName, out Exception exception, out string url)
        {
            var attemptNumber = 0;
            var attampsMaximum = SeleniumTestsConfiguration.TestAttempts + (SeleniumTestsConfiguration.FastMode ? 1 : 0);
            do
            {
                url = null;
                attemptNumber++;
                WriteLine($"Attamp #{attemptNumber} starts.");
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
                    url = TryGetBrowserCurrentUrl(wrapper.Browser);
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

                var filename = Path.Combine(ScreenshotsFolderPath, GetScreenshotName(attemptNumber));
                browserWrapper.TakeScreenshot(filename);
                LogCurrentlyPerformedAction($"Screenshot saved to: {filename}");
                Context.AddResultFile(filename);
            }
            catch (Exception ex)
            {
                Log("Screenshot CANNOT be saved!", 10);
                Log(ex);
            }
        }

        /// <summary>
        /// Get new name to save screenshot. 
        /// </summary>
        /// <param name="attemptNumber">number of test iteration (re-try iteration)</param>
        protected virtual string GetScreenshotName(int attemptNumber)
        {
            if (Context == null)
            {
                return $"{DateTime.Now:YYMMDD}_attempt{attemptNumber}.png";
            }
            return $"{Context.FullyQualifiedTestClassName}_{Context.TestName}" + attemptNumber + ".png";
        }

        /// <summary>
        /// Writes debug informations via loggers.
        /// </summary>
        protected static void WriteLine(string message, TraceLevel level = TraceLevel.Verbose)
        {
            Loggers.ForEach(logger =>
            {
                logger.WriteLine(message, level);
            });
        }

        /// <summary>
        /// Writes messages to registered loggers.
        /// </summary>
        /// <param name="message">Message that is going to be written by logger.</param>
        /// <param name="priority">Priority of message. 0 - the highest, 10 - the lowest = internal system log message level</param>
        /// <param name="level">Severity of a information.</param>
        public static void Log(string message, int priority = 0, TraceLevel level = TraceLevel.Verbose)
        {
            if (SeleniumTestsConfiguration.LoggingPriorityMaximum >= priority)
            {
                WriteLine(message, level);
            }
        }

        /// <summary>
        /// Writes exception to registered loggers.
        /// </summary>
        public virtual void Log(Exception exception)
        {
            WriteLine(exception.ToString(), System.Diagnostics.TraceLevel.Error);
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

        /// <summary>
        /// Clears global variables dependent on specific tests.
        /// </summary>
        /// <remarks>Automatically called on the end of <see cref="RunInAllBrowsers"/>.</remarks>
        public virtual void TestCleanUp()
        {
            AllowDerivedExceptionTypes = false;
            ExpectedExceptionType = null;
            Log("Test cleanup.");
            Loggers.ForEach(s=> s.OnTestFinished(Context));
        }

        /// <summary>
        /// Cleans all browsers.
        /// </summary>
        public virtual void TotalCleanUp()
        {
            Log("Class cleanup.");
            if (SeleniumTestsConfiguration.FastMode)
                FastModeFactoryRegistry.Dispose();
        }
    }
}