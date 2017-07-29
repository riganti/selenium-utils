using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public abstract class CoordinatorWebBrowserBase : WebBrowserBase
    {
        private CoordinatorWebBrowserFactory factory;

        public ContainerLeaseDataDTO Lease { get; }

        public CoordinatorWebBrowserBase(CoordinatorWebBrowserFactory factory, ContainerLeaseDataDTO lease)
        {
            this.factory = factory;

            Lease = lease;
        }
        
    }
}