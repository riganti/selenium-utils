using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Logging;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class FirefoxDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "firefox:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new FirefoxDevWebBrowser(this);
        }

        public FirefoxDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}