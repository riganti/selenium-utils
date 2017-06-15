using System.Collections.Generic;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core;

namespace Riganti.Utils.Testing.Selenium.BrowserStack
{
    public static class BrowserStackConfiguration
    {
        public static bool EnableBrowserStack => SeleniumTestsConfiguration.CheckAndGet(
            SeleniumTestsConfiguration.GetSettingsKey("BrowserStack"), false);
        public static bool BrowserStackOnly => SeleniumTestsConfiguration.CheckAndGet(
           SeleniumTestsConfiguration.GetSettingsKey("BrowserStackOnly"), true);
    }
}