using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore.BrowserStack
{
    public static class BrowserStackConfiguration
    {
        public static bool EnableBrowserStack => SeleniumTestsConfiguration.CheckAndGet(
            SeleniumTestsConfiguration.GetSettingsKey("BrowserStack"), false);
        public static bool BrowserStackOnly => SeleniumTestsConfiguration.CheckAndGet(
           SeleniumTestsConfiguration.GetSettingsKey("BrowserStackOnly"), true);
    }
}