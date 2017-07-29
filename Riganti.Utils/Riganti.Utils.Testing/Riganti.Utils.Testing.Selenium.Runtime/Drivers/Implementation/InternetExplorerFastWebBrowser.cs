using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class InternetExplorerFastWebBrowser : WebBrowserBase
    {
        private readonly LocalWebBrowserFactory factory;

        public InternetExplorerFastWebBrowser(LocalWebBrowserFactory factory)
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