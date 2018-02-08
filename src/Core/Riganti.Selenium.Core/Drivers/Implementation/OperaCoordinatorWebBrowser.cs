using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class OperaCoordinatorWebBrowser : CoordinatorWebBrowserBase
    {
        public OperaCoordinatorWebBrowser(CoordinatorWebBrowserFactoryBase factory, ContainerLeaseDataDTO lease) : base(factory, lease)
        {
        }

        protected override IWebDriver CreateDriver()
        {

            return new RemoteWebDriver(Lease.HubUri, new OperaOptions());
        }
    }
}
