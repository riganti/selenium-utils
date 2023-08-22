using System;
using System.IO;
using OpenQA.Selenium.Firefox;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class FirefoxHelpers
    {
        private static string pathToFirefoxBinary;
        
        private static FirefoxDriverService service;
        
        static FirefoxHelpers()
        {
            service = FirefoxDriverService.CreateDefaultService();
            // service.LogLevel = FirefoxDriverLogLevel.Trace;
        }

        public static FirefoxDriver CreateFirefoxDriver(LocalWebBrowserFactory factory)
        {
            factory.LogInfo($"Creating firefox driver from '{pathToFirefoxBinary}'.");

            if (!string.IsNullOrWhiteSpace(pathToFirefoxBinary))
            {
                return CreateAlternativeInstance();
            }

            try
            {
                return new FirefoxDriver(service, GetFirefoxOptions());
            }
            catch (Exception e)
            {
                factory.LogError("Firefox driver could not be created.")
                factory.LogError(e);

                var env = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                if (env.Contains("(x86)"))
                {
                    env = env.Replace("(x86)", "").Trim();
                }
                var firefox = "Mozilla Firefox\\Firefox.exe";
                if (File.Exists(Path.Combine(env, firefox)))
                {
                    return CreateAlternativeInstance(env, firefox);
                }

                env = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                if (File.Exists(Path.Combine(env, firefox)))
                {
                    return CreateAlternativeInstance(env, firefox);
                }
                throw;
            }
        }

        private static FirefoxDriver CreateAlternativeInstance(string env, string firefox)
        {
            pathToFirefoxBinary = Path.Combine(env, firefox);
            return CreateAlternativeInstance();
        }

        private static FirefoxDriver CreateAlternativeInstance()
        {
            var firefoxOptions = new FirefoxOptions()
            {
                BrowserExecutableLocation = pathToFirefoxBinary,
                Profile = GetFirefoxProfile()
            };
            return new FirefoxDriver(firefoxOptions);
        }

        public static FirefoxProfile GetFirefoxProfile()
        {
            var profile = new FirefoxProfile();
            profile.SetPreference("browser.privatebrowsing.autostart", true);
            profile.SetPreference("browser.privatebrowsing.dont_prompt_on_enter", true);
            profile.DeleteAfterUse = true;
            return profile;
        }

        public static FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions { Profile = GetFirefoxProfile() };
            return options;
        }
    }
}
