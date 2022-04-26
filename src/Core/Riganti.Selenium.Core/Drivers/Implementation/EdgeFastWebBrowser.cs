using OpenQA.Selenium;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class EdgeFastWebBrowser : FastWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory)base.Factory;

        public EdgeFastWebBrowser(IWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return EdgeHelpers.CreateEdgeDriver(Factory);
        }
    }
}
