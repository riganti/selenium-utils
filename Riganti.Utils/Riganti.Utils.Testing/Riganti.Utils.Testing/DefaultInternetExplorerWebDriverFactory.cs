using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultInternetExplorerWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            var driver = new InternetExplorerDriver();
            driver.SetDefaultTimeouts();
            return driver;
        }
    }
}