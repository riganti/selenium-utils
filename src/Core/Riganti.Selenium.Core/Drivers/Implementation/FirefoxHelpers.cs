using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;
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
            Console.WriteLine("Firefox applies capabilities: " + JsonConvert.SerializeObject(factory.Capabilities));
            var ffOptions = GetFirefoxOptions(factory.Options);
            Console.WriteLine("Firefox applies options: " + JsonConvert.SerializeObject(ffOptions));
            var driver = new FirefoxDriver(service, ffOptions);
            return driver;
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
            var options = new FirefoxOptions
            {
                //    Profile = GetFirefoxProfile()
            };
            options.BrowserVersion = _options.TryGet(nameof(options.BrowserVersion));
            Console.WriteLine($"Requesting {options.BrowserVersion} version of browser");
            return options;
        }
    }
}
