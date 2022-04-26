using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
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