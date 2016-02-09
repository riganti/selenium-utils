using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

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
                return PrepareDriver(new FirefoxDriver());
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
            return PrepareDriver(new FirefoxDriver(new FirefoxBinary(pathToFirefoxBinary), null));
        }

        public static IWebDriver PrepareDriver(IWebDriver driver)
        {

            driver.SetDefaultTimeouts();
            return driver;

        }
    }
}