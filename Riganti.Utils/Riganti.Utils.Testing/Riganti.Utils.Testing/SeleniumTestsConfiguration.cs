using System;
using System.Configuration;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public static class SeleniumTestsConfiguration
    {
        static SeleniumTestsConfiguration()
        {
            //test's common settings
            CheckAndSet(GetSettingsKey("ActionTimeout"), 100, value => ActionTimeout = value, false);
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
            CheckAndSet(GetSettingsKey("DeveloperMode"), true, value => { DeveloperMode = PlainMode = value; }, false);
        }

        #region Properties

        public static int ActionTimeout { get; set; }

        public static bool PlainMode { get; set; }

        public static bool FastMode { get; set; } = true;

        public static string BaseUrl { get; set; }
        public static bool StandardOutputLogger { get; set; }

        public const string ConfigurationAppSettingsKeyPrefix = "selenium:";
        public static bool StartChromeDriver { get; set; }
        public static bool StartInternetExplorerDriver { get; set; }
        public static bool StartFirefoxDriver { get; set; }
        public static int TestAttempts { get; set; } = 2;
        public static bool DeveloperMode { get; set; }
        public static bool DebugLogger { get; set; }
        public static bool DebugLoggerContainedKey { get; set; }
        public static bool DebuggerLogger { get; set; }
        public static bool TeamcityLogger { get; set; }
        public static bool TestContextLogger { get; set; }
        public static int LoggingPriorityMaximum { get; set; }

        #endregion Properties

        /// <summary>
        /// Check if key exists in appSettings and try to convert value and set it.
        /// </summary>
        /// <typeparam name="T">Supported types are only string, bool, int and double.</typeparam>
        /// <param name="setFunction">Delegate which is called after getting value to set value.</param>
        public static void CheckAndSet<T>(string key, T defaultValue, Action<T> setFunction, bool isKeyCaseSensitive = true)
        {
            setFunction(CheckAndGet(key, defaultValue, isKeyCaseSensitive));
        }

        /// <summary>
        /// Check if key exists in appSettings and try to convert value and set it.
        /// </summary>
        /// <typeparam name="T">Supported types are only string, bool, int and double.</typeparam>
        /// <param name="setFunction">Delegate which is called after getting value to set value.</param>
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
    }
}