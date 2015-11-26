using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultFirefoxWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            return new FirefoxDriver();
        }
    }
}