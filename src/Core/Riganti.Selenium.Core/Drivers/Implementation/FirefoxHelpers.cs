using System;
using System.IO;
using OpenQA.Selenium.Firefox;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class FirefoxHelpers
    {
        private static FirefoxDriverService service;

        static FirefoxHelpers()
        {
            service = FirefoxDriverService.CreateDefaultService();
        }

        public static FirefoxDriver CreateFirefoxDriver(LocalWebBrowserFactory factory)
        {
            return new FirefoxDriver(service, GetFirefoxOptions(factory.Options));
        }

        public static FirefoxProfile GetFirefoxProfile()
        {
            var profile = new FirefoxProfile();
            profile.SetPreference("browser.privatebrowsing.autostart", true);
            profile.SetPreference("browser.privatebrowsing.dont_prompt_on_enter", true);
            profile.DeleteAfterUse = true;
            return profile;
        }

        public static FirefoxOptions GetFirefoxOptions(System.Collections.Generic.IDictionary<string, string> _options)
        {
            var options = new FirefoxOptions { Profile = GetFirefoxProfile() };
            options.BrowserVersion = _options.TryGetOrDefault(nameof(options.BrowserVersion), "stable");
            return options;
        }
    }
}
