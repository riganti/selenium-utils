using OpenQA.Selenium;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class ChromeDevWebBrowser : DevWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory)base.Factory;

        public ChromeDevWebBrowser(LocalWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return ChromeHelpers.CreateChromeDriver(Factory);
        }
    }
}
