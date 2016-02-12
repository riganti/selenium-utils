using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            var driver = new ChromeDriver(options);

            driver.SetDefaultTimeouts();
            return driver;
        }
    }
}