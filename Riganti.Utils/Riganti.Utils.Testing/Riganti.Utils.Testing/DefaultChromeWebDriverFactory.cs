using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            if (SeleniumTestsConfiguration.ChromeDriverIncognito)
            {
                options.AddArgument("-incognito");
            }

            var driver = new ChromeDriverWrapper(options);
            driver.SetDefaultTimeouts();
            return driver;
        }
    }
}