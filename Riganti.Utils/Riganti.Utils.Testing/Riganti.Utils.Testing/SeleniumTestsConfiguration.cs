using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class SeleniumTestsConfiguration
    {
        static SeleniumTestsConfiguration()
        {
            var keys = ConfigurationManager.AppSettings.AllKeys.Where(s => s.Contains(ConfigurationAppSettingsKeyPrefix));
            foreach (var key in keys)
            {
                var value = ConfigurationManager.AppSettings[key];
                if (string.Equals(key, GetSettingsKey("ChromeDriver"), StringComparison.OrdinalIgnoreCase))
                {
                    StartChromeDriver = TryParseBool(value);
                }
                if (string.Equals(key, GetSettingsKey("InternetExplorerDriver"), StringComparison.OrdinalIgnoreCase) || string.Equals(key, GetSettingsKey("IeDriver"), StringComparison.OrdinalIgnoreCase))
                {
                    StartInternetExplorerDriver = TryParseBool(value);
                }
                if (string.Equals(key, GetSettingsKey("FirefoxDriver"), StringComparison.OrdinalIgnoreCase))
                {
                    StartFirefoxDriver = TryParseBool(value);
                }
                if (string.Equals(key, GetSettingsKey("BaseUrl"), StringComparison.OrdinalIgnoreCase))
                {
                    BaseUrl = value;
                }
                if (string.Equals(key, GetSettingsKey("FastMode"), StringComparison.OrdinalIgnoreCase))
                {
                    FastMode = TryParseBool(value, true);
                }
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
        }

        public static bool FastMode { get; set; } = true;

        public static string BaseUrl { get; set; }

        public const string ConfigurationAppSettingsKeyPrefix = "selenium:";
        public static bool StartChromeDriver { get; }
        public static bool StartInternetExplorerDriver { get; }
        public static bool StartFirefoxDriver { get; }
        public static int TestAttemps { get; } = 2;

        private static bool TryParseBool(string value, bool defaultValue = false)
        {
            bool tmp;
            if (!bool.TryParse(value, out tmp))
            {
                tmp = defaultValue;
            }
            return tmp;
        }

        private static string GetSettingsKey(string key)
        {
            return $"{ConfigurationAppSettingsKeyPrefix}{key}";
        }
    }
}