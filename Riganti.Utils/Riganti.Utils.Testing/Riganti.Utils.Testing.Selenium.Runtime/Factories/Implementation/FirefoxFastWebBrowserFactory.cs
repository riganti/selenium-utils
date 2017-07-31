using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
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