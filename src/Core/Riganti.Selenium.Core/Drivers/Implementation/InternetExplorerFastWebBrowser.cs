using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class InternetExplorerFastWebBrowser : FastWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory) base.Factory;

        public InternetExplorerFastWebBrowser(LocalWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return InternetExplorerHelpers.CreateInternetExplorerDriver(Factory);
        }
        
    }
}