using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.AssertApi
{
    public class BrowserWrapperAssertApi :BrowserWrapper
    {
        public BrowserWrapperAssertApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }

    
    }
}