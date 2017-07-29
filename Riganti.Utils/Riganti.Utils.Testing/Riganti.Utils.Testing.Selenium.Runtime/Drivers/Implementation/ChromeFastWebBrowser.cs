using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class ChromeFastWebBrowser : WebBrowserBase
    {
        private readonly LocalWebBrowserFactory factory;

        public ChromeFastWebBrowser(LocalWebBrowserFactory factory)
        {
            this.factory = factory;
        }
         

        protected override IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");

            if (Convert.ToBoolean(factory.Options["DisableExtensions"] ?? "true"))
            {
                options.AddArgument("--disable-extensions");
            }

            return new ChromeDriver(options);
        }

        public override void ClearBrowserState()
        {
            throw new NotImplementedException();
        }
    }
}