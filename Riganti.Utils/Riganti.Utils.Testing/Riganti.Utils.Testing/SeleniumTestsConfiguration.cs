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
                if (string.Equals(key, GetSettingsKey("chromedriver"), StringComparison.OrdinalIgnoreCase))
                {
                    StartChromeDriver = TryParseBool(value);
                }
                if (string.Equals(key, GetSettingsKey("InternetExplorerDriver"), StringComparison.OrdinalIgnoreCase))
                {
                    StartInternetExplorerDriver = TryParseBool(value);
                }
                if (string.Equals(key, GetSettingsKey("firefoxdriver"), StringComparison.OrdinalIgnoreCase))
                {
                    StartFirefoxDriver = TryParseBool(value);
                }
                if (string.Equals(key, GetSettingsKey("testattemptscount"), StringComparison.OrdinalIgnoreCase))
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

        public const string ConfigurationAppSettingsKeyPrefix = "selenium:";
        public static bool StartChromeDriver { get; }
        public static bool StartInternetExplorerDriver { get; }
        public static bool StartFirefoxDriver { get; }
        public static int TestAttemps { get; }

        private static bool TryParseBool(string value)
        {
            bool tmp;
            bool.TryParse(value, out tmp);
            return tmp;
        }

        private static string GetSettingsKey(string key)
        {
            return $"{ConfigurationAppSettingsKeyPrefix}{key}";
        }
    }
}