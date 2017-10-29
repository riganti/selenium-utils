using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation
{
    public class EdgeDevWebBrowser : DevWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory)base.Factory;

        public EdgeDevWebBrowser(IWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return EdgeHelpers.CreateEdgeDriver(Factory);
        }
    }
}
