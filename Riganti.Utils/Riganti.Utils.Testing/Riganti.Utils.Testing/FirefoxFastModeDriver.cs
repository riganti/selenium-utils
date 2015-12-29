using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class FirefoxFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            return new DefaultFirefoxWebDriverFactory().CreateNewInstance();
        }
    }
}