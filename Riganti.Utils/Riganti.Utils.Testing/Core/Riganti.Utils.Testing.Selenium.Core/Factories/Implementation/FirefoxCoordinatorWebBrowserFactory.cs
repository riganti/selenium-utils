using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
{
    public class FirefoxCoordinatorWebBrowserFactory : CoordinatorWebBrowserFactoryBase
    {
        public override string Name => "firefox:coordinator";

        protected override string BrowserType => "firefox";

        protected override CoordinatorWebBrowserBase CreateBrowserCore(ContainerLeaseDataDTO lease)
        {
            return new FirefoxCoordinatorWebBrowser(this, lease);
        }

        public FirefoxCoordinatorWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}