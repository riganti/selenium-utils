using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation
{
    public class FirefoxFastWebBrowser : FastWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory)base.Factory;

        public FirefoxFastWebBrowser(LocalWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            throw new NotImplementedException();
        }
        
    }
}