using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
{
    public class FirefoxCoordinatorWebBrowserFactory : CoordinatorWebBrowserFactoryBase
    {
        public override string Name => "firefox:coordinator";

        protected override string BrowserType => "firefox";

        protected override CoordinatorWebBrowserBase CreateBrowserCore(ContainerLeaseDataDTO lease)
        {
            return new FirefoxCoordinatorWebBrowser(this, lease);
        }

        public FirefoxCoordinatorWebBrowserFactory(SeleniumTestsConfiguration configuration, LoggerService loggerService, TestContextAccessor testContextAccessor) : base(configuration, loggerService, testContextAccessor)
        {
        }
    }
}