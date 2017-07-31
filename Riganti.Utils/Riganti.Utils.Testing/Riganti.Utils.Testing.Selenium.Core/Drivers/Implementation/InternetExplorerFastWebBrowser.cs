using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation
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