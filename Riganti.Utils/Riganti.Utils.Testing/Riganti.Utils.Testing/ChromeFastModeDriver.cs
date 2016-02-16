using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ChromeFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            return new DefaultChromeWebDriverFactory().CreateNewInstance();
        }
    }
}