using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class FirefoxDevWebBrowser : DevWebBrowserBase
    {
        private readonly LocalWebBrowserFactory factory;

        public FirefoxDevWebBrowser(LocalWebBrowserFactory factory)
        {
            this.factory = factory;
        }

        protected override IWebDriver CreateDriver()
        {
            throw new NotImplementedException();
        }
    }
}