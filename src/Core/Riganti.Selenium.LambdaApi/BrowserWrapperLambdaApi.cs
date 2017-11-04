using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.LambdaApi
{
    public class BrowserWrapperLambdaApi : BrowserWrapper
    {
        public BrowserWrapperLambdaApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }

    }
}
