using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation
{
    public class FirefoxDevWebBrowser : DevWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory)base.Factory;

        public FirefoxDevWebBrowser(LocalWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return FirefoxHelpers.CreateFirefoxDriver(Factory);
        }
    }
}