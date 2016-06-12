using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class InternetExplorerFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            return new DefaultInternetExplorerWebDriverFactory().CreateNewInstance();
        }
    }
}