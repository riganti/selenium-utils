using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.LambdaApi
{
    public class BrowserWrapperLambdaApi : BrowserWrapper
    {
        public BrowserWrapperLambdaApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }

    }
}
