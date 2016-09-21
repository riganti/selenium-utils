using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            if (SeleniumTestsConfiguration.ChromeDriverIncognito)
            {
                options.AddArgument("--disable-extensions");
            }

            var driver = new ChromeDriverWrapper(options);
            driver.SetDefaultTimeouts();
            return driver;
        }
    }
}