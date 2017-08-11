using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
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
