using System;
using System.Configuration;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public static class SeleniumTestsConfiguration
    {
        static SeleniumTestsConfiguration()
        {
            var keys = ConfigurationManager.AppSettings.AllKeys.Where(s => s.Contains(ConfigurationAppSettingsKeyPrefix));
            foreach (var key in keys)
            {
                var value = ConfigurationManager.AppSettings[key];
                CheckAndSetChromeDriver(key, value);
                CheckAndSetIeDriver(key, value);
                CheckAndSetFirefoxDriver(key, value);
                CheckAndSetBaseUrl(key, value);
                CheckAndSetFastMode(key, value);
                CheckAndSetDeveloperMode(key, value);
                CheckAndSetTestAtampsCount(key, value);
                CheckAndSetStandardOutputLogger(key, value);
                CheckAndSetTeamcityLogger(key, value);
                CheckAndSetTestContextLogger(key, value);
                CheckAndSetDebuggerLogger(key, value);
                CheckAndSetDebugLogger(key, value);
                CheckAndSetLoggingPriorityMaximum(key, value);
            }
        }

        private static void CheckAndSetLoggingPriorityMaximum(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("LoggingPriorityMaximum"), StringComparison.OrdinalIgnoreCase))
            {
                LoggingPriorityMaximum = TryParseInt(value, 10);
            }
        }

        #region check functions

        private static void CheckAndSetTeamcityLogger(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("TeamcityLogger"), StringComparison.OrdinalIgnoreCase))
            {
                TeamcityLogger = TryParseBool(value);
            }
        }

        private static void CheckAndSetTestContextLogger(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("TestContextLogger"), StringComparison.OrdinalIgnoreCase))
            {
                TestContextLogger = TryParseBool(value);
            }
        }

        private static void CheckAndSetDebugLogger(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("DebugLogger"), StringComparison.OrdinalIgnoreCase))
            {
                DebugLoggerContainedKey = true;
                DebugLogger = TryParseBool(value);
            }
        }

        private static void CheckAndSetDebuggerLogger(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("DebuggerLogger"), StringComparison.OrdinalIgnoreCase))
            {
                DebuggerLogger = TryParseBool(value);
            }
        }

        private static void CheckAndSetStandardOutputLogger(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("StandardOutputLogger"), StringComparison.OrdinalIgnoreCase))
            {
                StandardOutputLogger = TryParseBool(value);
            }
        }

        public static bool StandardOutputLogger { get; set; }

        private static void CheckAndSetTestAtampsCount(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("TestAttemptsCount"), StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    TestAttemps = int.Parse(value);
                }
                catch (Exception)
                {
                    TestAttemps = 2;
                }
                if (TestAttemps <= 0)
                {
                    throw new ConfigurationErrorsException($@"Value of '{ConfigurationAppSettingsKeyPrefix}TestAttemptsCount' must be greater than 0.");
                }
            }
        }

        private static void CheckAndSetChromeDriver(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("ChromeDriver"), StringComparison.OrdinalIgnoreCase))
            {
                StartChromeDriver = TryParseBool(value);
            }
        }

        private static void CheckAndSetIeDriver(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("InternetExplorerDriver"), StringComparison.OrdinalIgnoreCase) ||
                string.Equals(key, GetSettingsKey("IeDriver"), StringComparison.OrdinalIgnoreCase))
            {
                StartInternetExplorerDriver = TryParseBool(value);
            }
        }

        private static void CheckAndSetFirefoxDriver(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("FirefoxDriver"), StringComparison.OrdinalIgnoreCase))
            {
                StartFirefoxDriver = TryParseBool(value);
            }
        }

        private static void CheckAndSetBaseUrl(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("BaseUrl"), StringComparison.OrdinalIgnoreCase))
            {
                BaseUrl = value;
            }
        }

        private static void CheckAndSetFastMode(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("FastMode"), StringComparison.OrdinalIgnoreCase))
            {
                FastMode = TryParseBool(value, true);
            }
        }

        private static void CheckAndSetDeveloperMode(string key, string value)
        {
            if (string.Equals(key, GetSettingsKey("DeveloperMode"), StringComparison.OrdinalIgnoreCase))
            {
                PlainMode = DeveloperMode = TryParseBool(value);
            }
        }

        public static bool PlainMode { get; set; }

        #endregion check functions

        #region Properties

        public static bool FastMode { get; set; } = true;

        public static string BaseUrl { get; set; }

        public const string ConfigurationAppSettingsKeyPrefix = "selenium:";
        public static bool StartChromeDriver { get; set; }
        public static bool StartInternetExplorerDriver { get; set; }
        public static bool StartFirefoxDriver { get; set; }
        public static int TestAttemps { get; set; } = 2;
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
            var filteredKey = ConfigurationManager.AppSettings.AllKeys.FirstOrDefault(s => s.Equals(key, isKeyCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));

            //if null set default value
            if (filteredKey == null)
            {
                if (defaultValue != null)
                {
                    setFunction(defaultValue);
                }
                return;
            }
            // for bool
            if (typeof(T) == typeof(bool))
            {
                setFunction((T)(object)TryParseBool(ConfigurationManager.AppSettings[filteredKey], (defaultValue as bool?) ?? false));
                return;
            }
            //for string
            if (typeof(T) == typeof(string))
            {
                var value = (T)(object)ConfigurationManager.AppSettings[filteredKey];
                if (value == null)
                {
                    value = defaultValue;
                }
                setFunction(value);
                return;
            }
            // for int
            if (typeof(T) == typeof(int))
            {
                setFunction((T)(object)TryParseInt(ConfigurationManager.AppSettings[filteredKey], (defaultValue as int?) ?? 0));
                return;
            }
            // for double
            if (typeof(T) == typeof(double))
            {
                setFunction((T)(object)TryParseDouble(ConfigurationManager.AppSettings[filteredKey], (defaultValue as double?) ?? 0));
                return;
            }
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