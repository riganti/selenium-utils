using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultFirefoxWebDriverFactory : IWebDriverFactory
    {
        private static string pathToFirefoxBinary;

        public IWebDriver CreateNewInstance()
        {
            if (!string.IsNullOrWhiteSpace(pathToFirefoxBinary))
            {
                return PrepareDriver(AlternativeInstance());
            }

            try
            {
                return PrepareDriver(new FirefoxDriver(GetProfile()));
            }
            catch
            {
                SeleniumTestBase.Log("Default location of firefox was not found.");
                var env = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                if (env.Contains("(x86)"))
                {
                    env = env.Replace("(x86)", "").Trim();
                }
                var firefox = "Mozilla Firefox\\Firefox.exe";
                if (File.Exists(Path.Combine(env, firefox)))
                {
                    return PrepareDriver(AlternativeInstance(env, firefox));
                }

                env = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                if (File.Exists(Path.Combine(env, firefox)))
                {
                    return PrepareDriver(AlternativeInstance(env, firefox));
                }
                throw;
            }
        }

        private static IWebDriver AlternativeInstance(string env, string firefox)
        {
            pathToFirefoxBinary = Path.Combine(env, firefox);
            SeleniumTestBase.Log($"Setting up new firefox binary file path to {pathToFirefoxBinary}");
            return PrepareDriver(AlternativeInstance());
        }

        private static IWebDriver AlternativeInstance()
        {
            return PrepareDriver(new FirefoxDriver(new FirefoxBinary(pathToFirefoxBinary), GetProfile()));
        }

        public static IWebDriver PrepareDriver(IWebDriver driver)
        {
            driver.SetDefaultTimeouts();
            return driver;
        }

        public static FirefoxProfile GetProfile()
        {
            var profile = new FirefoxProfile();
            profile.SetPreference("browser.privatebrowsing.autostart", true);
            profile.SetPreference("browser.privatebrowsing.dont_prompt_on_enter", true);
            profile.AcceptUntrustedCertificates = true;
            profile.DeleteAfterUse = true;
            return profile;
        }
    }
}