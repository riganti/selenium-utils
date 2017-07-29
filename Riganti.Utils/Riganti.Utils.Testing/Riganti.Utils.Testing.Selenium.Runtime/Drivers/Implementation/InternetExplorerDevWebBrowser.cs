using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class InternetExplorerDevWebBrowser : DevWebBrowserBase
    {
        private LocalWebBrowserFactory factory;

        public InternetExplorerDevWebBrowser(LocalWebBrowserFactory factory)
        {
            this.factory = factory;
        }

        protected override IWebDriver CreateDriver()
        {
            throw new NotImplementedException();
        }
    }
}
