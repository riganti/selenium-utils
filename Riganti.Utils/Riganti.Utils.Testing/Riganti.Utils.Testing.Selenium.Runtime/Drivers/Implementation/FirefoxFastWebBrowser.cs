using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class FirefoxFastWebBrowser : WebBrowserBase
    {
        private readonly LocalWebBrowserFactory factory;

        public FirefoxFastWebBrowser(LocalWebBrowserFactory factory)
        {
            this.factory = factory;
        }

        protected override IWebDriver CreateDriver()
        {
            throw new NotImplementedException();
        }

        public override void ClearBrowserState()
        {
            throw new NotImplementedException();
        }
    }
}