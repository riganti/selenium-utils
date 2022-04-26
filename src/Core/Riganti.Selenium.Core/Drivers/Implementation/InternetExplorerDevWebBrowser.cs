using OpenQA.Selenium;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class InternetExplorerDevWebBrowser : DevWebBrowserBase
    {
        public new LocalWebBrowserFactory Factory => (LocalWebBrowserFactory)base.Factory;

        public InternetExplorerDevWebBrowser(LocalWebBrowserFactory factory) : base(factory)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return InternetExplorerHelpers.CreateInternetExplorerDriver(Factory);
        }
    }
}
