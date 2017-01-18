using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public static class SeleniumTestsConfiguration
    {
        static SeleniumTestsConfiguration()
        {
            //test's common settings
            CheckAndSet(GetSettingsKey("ActionTimeout"), 250, value => ActionTimeout = value, false);
            CheckAndSet(GetSettingsKey("LoggingPriorityMaximum"), 9, value => LoggingPriorityMaximum = value, false);
            CheckAndSet(GetSettingsKey("TestAttemptsCount"), 2, value => TestAttempts = value, false);
            CheckAndSet<string>(GetSettingsKey("BaseUrl"), null, value => BaseUrl = value, false);

            if (TestAttempts <= 0)
            {
                throw new ConfigurationErrorsException($@"Value of '{ConfigurationAppSettingsKeyPrefix}TestAttemptsCount' must be greater than 0.");
            }

            // loggers
            CheckAndSet(GetSettingsKey("DebuggerLogger"), false, value => DebuggerLogger = value, false);
            CheckAndSet(GetSettingsKey("TestContextLogger"), false, value => TestContextLogger = value, false);
            CheckAndSet(GetSettingsKey("StandardOutputLogger"), false, value => StandardOutputLogger = value, false);
            CheckAndSet(GetSettingsKey("DebugLogger"), false, value => DebugLogger = value, false);
            DebugLoggerContainedKey = ConfigurationManager.AppSettings.AllKeys.Any(s => string.Equals(GetSettingsKey("DebugLogger"), s, StringComparison.OrdinalIgnoreCase));

            //drivers
            CheckAndSet(GetSettingsKey("ChromeDriver"), false, value => StartChromeDriver = value, false);
            CheckAndSet(GetSettingsKey("FirefoxDriver"), false, value => StartFirefoxDriver = value, false);
            StartInternetExplorerDriver = CheckAndGet(GetSettingsKey("InternetExplorerDriver"), false, false) || CheckAndGet(GetSettingsKey("IeDriver"), false, false);

            // modes
            CheckAndSet(GetSettingsKey("FastMode"), true, value => FastMode = value, false);
            CheckAndSet(GetSettingsKey("DeveloperMode"), false, value => { DeveloperMode = PlainMode = value; }, false);

            //advanced
            CheckAndSet(GetSettingsKey("TryToKillWhenNotResponding"), false, value => { TryToKillWhenNotResponding = value; }, false);

            // chrome driver settings
            CheckAndSet(GetSettingsKey("ChromeDriverIncognito"), false, value => { ChromeDriverIncognito = value; }, false);
        }

        #region Properties

        /// <summary>
        /// Sets default timeout between element actions (Click, SendKeys etc..)
        /// </summary>
        public static int ActionTimeout { get; set; }

        /// <summary>
        /// Internal developer mode for tests.
        /// </summary>
        public static bool PlainMode { get; set; }

        /// <summary>
        /// When FastMode=true the test base class do not close the browser after each test. It tries to reuse the opened browsers as much as possible. After all tests are finished the base class closes all opened drivers (browsers).
        /// </summary>
        /// <remarks>
        /// The FastMode clears cookies, storages, etc. after every test method run. When test method throws exception, driver is disposed and test method is retried.
        /// </remarks>
        public static bool FastMode { get; set; } = true;

        /// <summary>
        /// BaseUrl is used for <see cref="BrowserWrapper.NavigateToUrl(string)"/> mehtod. You can specify base url and then you can use relative addresses in tests.
        /// </summary>
        public static string BaseUrl { get; set; }

        /// <summary>
        /// When StandardOutputLogger=true then the <see cref="SeleniumTestBase.Log(string,int)" /> method writes to standard output by <see cref="Console.Write()"/>
        /// </summary>
        public static bool StandardOutputLogger { get; set; }

        /// <summary>
        /// Configuration prefix for appSettings in app.config
        /// </summary>
        public const string ConfigurationAppSettingsKeyPrefix = "selenium:";

        /// <summary>
        /// Adds Chrome Driver with default settings to list of browsers.
        /// </summary>
        public static bool StartChromeDriver { get; set; }

        /// <summary>
        /// Adds IE Driver with default settings to list of browsers.
        /// </summary>
        public static bool StartInternetExplorerDriver { get; set; }

        /// <summary>
        /// Adds Firefox Driver with default settings to list of browsers.
        /// </summary>
        public static bool StartFirefoxDriver { get; set; }

        /// <summary>
        /// Determines how many times test is tried to execute when it fails.
        /// </summary>
        public static int TestAttempts { get; set; } = 2;

        /// <summary>
        /// Changes the Execution plan. TestAttempts, ExpectedException is ignored. All thrown exceptions are NOT wrapped. Use only when creating tests. DO NOT USE FOR CONTINUES INTEGRATION.
        /// </summary>
        public static bool DeveloperMode { get; set; }

        /// <summary>
        /// When DebugLogger=true then the <see cref="SeleniumTestBase.Log(string,int)" /> method writes to debug output by <see cref="Debug.WriteLine(string)"/>
        /// </summary>
        public static bool DebugLogger { get; set; }

        public static bool DebugLoggerContainedKey { get; set; }

        /// <summary>
        /// When DebuggerLogger=true then the <see cref="SeleniumTestBase.Log(string,int)" /> method writes to debugger output by <see cref="Debugger.Log"/>
        /// </summary>
        public static bool DebuggerLogger { get; set; }

        /// <summary>
        /// When TraceLogger=true then the <see cref="SeleniumTestBase.Log(string,int)" /> method writes to trace output by <see cref="Trace.WriteLine(string)"/>
        /// </summary>
        public static bool TraceLogger { get; set; }

        /// <summary>
        /// When TeamcityLogger=true then the <see cref="SeleniumTestBase.Log(string,int)" /> method wrappes all messages for integration with TeamCity
        /// </summary>
        public static bool TeamcityLogger { get; set; }

        /// <summary>
        /// When DebuggerLogger=true then the <see cref="SeleniumTestBase.Log(string,int)" /> method writes to Test Context output by <see cref="TestContext.WriteLine(string)"/>
        /// </summary>
        public static bool TestContextLogger { get; set; }

        /// <summary>
        /// Sets maximum priority of logged messages to be written by <see cref="SeleniumTestBase.Log(string,int)" /> method.
        /// </summary>
        /// <remarks>The priority of internal log messages is 10.</remarks>
        public static int LoggingPriorityMaximum { get; set; }

        /// <remarks>Sets whether default settings for Chrome Driver includes Incognito mode.</remarks>
        public static bool ChromeDriverIncognito { get; set; }

        /// <summary>
        /// When browser cannot be closed and driver is not responding, this option tries to get PID of the driver and kill the process.
        /// </summary>
        public static bool TryToKillWhenNotResponding { get; set; }

        #endregion Properties

        #region Func

        /// <summary>
        /// Check if key exists in appSettings and try to convert value and set it.
        /// </summary>
        /// <typeparam name="T">Supported types are only string, bool, int and double.</typeparam>
        /// <param name="key">It's value of appSettings key attribute.</param>
        /// <param name="setFunction">Delegate which is called after getting value to set value.</param>
        /// <param name="isKeyCaseSensitive">Indicates whather recognition of key is case sensitive.</param>
        /// <param name="defaultValue">Default value when key is not in appSettings section.</param>
        public static void CheckAndSet<T>(string key, T defaultValue, Action<T> setFunction, bool isKeyCaseSensitive = true)
        {
            setFunction(CheckAndGet(key, defaultValue, isKeyCaseSensitive));
        }

        /// <summary>
        /// Check if key exists in appSettings and try to convert value and set it.
        /// </summary>
        /// <typeparam name="T">Supported types are only string, bool, int and double.</typeparam>
        public static T CheckAndGet<T>(string key, T defaultValue, bool isKeyCaseSensitive = true)
        {
            var filteredKey = ConfigurationManager.AppSettings.AllKeys.FirstOrDefault(s => s.Equals(key, isKeyCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));

            //if null set default value
            if (filteredKey == null)
            {
                if (defaultValue != null)
                {
                    return defaultValue;
                }
            }
            // for bool
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)TryParseBool(ConfigurationManager.AppSettings[filteredKey], (defaultValue as bool?) ?? false);
            }
            //for string
            if (typeof(T) == typeof(string))
            {
                var value = (T)(object)ConfigurationManager.AppSettings[filteredKey];
                if (value == null)
                {
                    value = defaultValue;
                }
                return value;
            }
            // for int
            if (typeof(T) == typeof(int))
            {
                return (T)(object)TryParseInt(ConfigurationManager.AppSettings[filteredKey], (defaultValue as int?) ?? 0);
            }
            // for double
            if (typeof(T) == typeof(double))
            {
                return (T)(object)TryParseDouble(ConfigurationManager.AppSettings[filteredKey], (defaultValue as double?) ?? 0);
            }
            return defaultValue;
        }

        private static double TryParseDouble(string value, double defaultValue = 0)
        {
            double max;
            if (double.TryParse(value, out max))
            {
                return max;
            }
            else
            {
                return defaultValue;
            }
        }

        public static bool TryParseBool(string value, bool defaultValue = false)
        {
            bool tmp;
            if (!bool.TryParse(value, out tmp))
            {
                tmp = defaultValue;
            }
            return tmp;
        }

        public static int TryParseInt(string value, int defaultValue = 0)
        {
            int max;
            if (int.TryParse(value, out max))
            {
                return max;
            }
            else
            {
                return defaultValue;
            }
        }

        public static string GetSettingsKey(string key)
        {
            return $"{ConfigurationAppSettingsKeyPrefix}{key}";
        }

        #endregion Func
    }
}