using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
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