using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class InternetExplorerFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            return new DefaultInternetExplorerWebDriverFactory().CreateNewInstance();
        }
    }
}