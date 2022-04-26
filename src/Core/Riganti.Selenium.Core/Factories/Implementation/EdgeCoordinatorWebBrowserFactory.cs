using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class EdgeCoordinatorWebBrowserFactory : CoordinatorWebBrowserFactoryBase
    {
        public override string Name => "edge:coordinator";

        protected override string BrowserType => "edge";

        protected override CoordinatorWebBrowserBase CreateBrowserCore(ContainerLeaseDataDTO lease)
        {
            return new EdgeCoordinatorWebBrowser(this, lease);
        }

        public EdgeCoordinatorWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
