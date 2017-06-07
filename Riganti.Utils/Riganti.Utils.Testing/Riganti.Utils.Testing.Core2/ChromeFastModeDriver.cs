using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class ChromeFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            return new DefaultChromeWebDriverFactory().CreateNewInstance();
        }
    }
}