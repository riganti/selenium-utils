using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
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