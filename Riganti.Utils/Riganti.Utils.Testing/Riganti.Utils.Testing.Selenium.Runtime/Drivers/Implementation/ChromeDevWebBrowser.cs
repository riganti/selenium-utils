using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class ChromeDevWebBrowser : DevWebBrowserBase
    {
        private readonly LocalWebBrowserFactory factory;

        public ChromeDevWebBrowser(LocalWebBrowserFactory factory)
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
    }
}
