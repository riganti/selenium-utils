using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class FirefoxFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            return new DefaultFirefoxWebDriverFactory().CreateNewInstance();
        }
    }
}