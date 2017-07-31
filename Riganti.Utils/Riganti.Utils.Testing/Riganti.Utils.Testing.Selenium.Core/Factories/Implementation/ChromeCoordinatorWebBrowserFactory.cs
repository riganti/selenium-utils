using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
{
    public class ChromeCoordinatorWebBrowserFactory : CoordinatorWebBrowserFactoryBase
    {
        public override string Name => "chrome:coordinator";

        protected override string BrowserType => "chrome";

        protected override CoordinatorWebBrowserBase CreateBrowserCore(ContainerLeaseDataDTO lease)
        {
            return new ChromeCoordinatorWebBrowser(this, lease);
        }

        public ChromeCoordinatorWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}