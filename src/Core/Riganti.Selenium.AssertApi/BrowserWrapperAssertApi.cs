using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.AssertApi
{
    public class BrowserWrapperAssertApi :BrowserWrapper
    {
        public BrowserWrapperAssertApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }

    
    }
}