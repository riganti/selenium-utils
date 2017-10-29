using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.LambdaApi
{
    public class BrowserWrapperLambdaApi : BrowserWrapper
    {
        public BrowserWrapperLambdaApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }

    }
}
