using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public abstract class CoordinatorWebBrowserBase : WebBrowserBase
    {
        public new CoordinatorWebBrowserFactoryBase Factory => (CoordinatorWebBrowserFactoryBase) base.Factory;

        public ContainerLeaseDataDTO Lease { get; }

        public CoordinatorWebBrowserBase(CoordinatorWebBrowserFactoryBase factory, ContainerLeaseDataDTO lease) : base(factory)
        {
            Lease = lease;
        }
    }
}