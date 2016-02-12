using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            SeleniumTestBase.Log("Creating new ChromeDriver");
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            var driver = new ChromeDriverWrapper(options);
            SeleniumTestBase.Log($"New ChromeDriver ID = {driver.GetDriverId()}");

            driver.SetDefaultTimeouts();
            return driver;
        }
    }

    public static class ExtensionsIWebDriver
    {
        public static Guid GetDriverId(this IWebDriver driver)
        {
            if (driver is ChromeDriverWrapper)
            {
                return ((ChromeDriverWrapper)driver).DriverInstanceId;
            }
            return Guid.Empty;
        }
    }

    public class ChromeDriverWrapper : ChromeDriver
    {
        public Guid DriverInstanceId { get; } = Guid.NewGuid();

        public ChromeDriverWrapper()
        {
        }

        public ChromeDriverWrapper(string chromeDriverDirectory) : base(chromeDriverDirectory)
        {
        }

        public ChromeDriverWrapper(ChromeDriverService service) : base(service)
        {
        }

        public ChromeDriverWrapper(ChromeOptions options) : base(options)
        {
        }

        public ChromeDriverWrapper(ChromeDriverService service, ChromeOptions options) : base(service, options)
        {
        }

        public ChromeDriverWrapper(string chromeDriverDirectory, ChromeOptions options) : base(chromeDriverDirectory, options)
        {
        }

        public ChromeDriverWrapper(ChromeDriverService service, ChromeOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
        {
        }

        public ChromeDriverWrapper(string chromeDriverDirectory, ChromeOptions options, TimeSpan commandTimeout) : base(chromeDriverDirectory, options, commandTimeout)
        {
        }
    }
}