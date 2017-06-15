using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class DefaultFirefoxWebDriverFactory : IWebDriverFactory
    {
        private static string pathToFirefoxBinary { get; set; }

        public IWebDriver CreateNewInstance()
        {
            if (!string.IsNullOrWhiteSpace(pathToFirefoxBinary))
            {
                return PrepareDriver(AlternativeInstance());
            }

            try
            {
                return PrepareDriver(new FirefoxDriverWrapper(GetProfile()));
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
            return PrepareDriver(new FirefoxDriverWrapper(new FirefoxBinary(pathToFirefoxBinary), GetProfile()));
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

    public class FirefoxDriverWrapper : FirefoxDriver,  IWebDriverWrapper 
    {

        public FirefoxDriverWrapper()
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

        public FirefoxDriverWrapper(FirefoxProfile profile) : base(profile)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

#pragma warning disable 618
        [Obsolete]
        public FirefoxDriverWrapper(ICapabilities capabilities) : base(capabilities)
#pragma warning restore 618
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

#pragma warning disable 618
        [Obsolete]
        public FirefoxDriverWrapper(FirefoxBinary binary, FirefoxProfile profile) : base(binary, profile)
#pragma warning restore 618
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

#pragma warning disable 618
        [Obsolete]
        public FirefoxDriverWrapper(FirefoxBinary binary, FirefoxProfile profile, TimeSpan commandTimeout) : base(binary, profile, commandTimeout)
#pragma warning restore 618
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

        public FirefoxDriverWrapper(FirefoxOptions options) : base(options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

        public FirefoxDriverWrapper(FirefoxDriverService service) : base(service)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

        public FirefoxDriverWrapper(FirefoxDriverService service, FirefoxOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - FirefoxDriver");
        }

        public Guid DriverId { get; } = Guid.NewGuid();

        public bool Disposed { get; set; }
    }

}