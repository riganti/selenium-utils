using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class FirefoxFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "firefox:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new FirefoxFastWebBrowser(this);
        }

        public FirefoxFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}