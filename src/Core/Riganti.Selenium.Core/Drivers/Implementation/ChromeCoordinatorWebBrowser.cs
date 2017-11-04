using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class ChromeCoordinatorWebBrowser : CoordinatorWebBrowserBase
    {
        public ChromeCoordinatorWebBrowser(CoordinatorWebBrowserFactoryBase factory, ContainerLeaseDataDTO lease) : base(factory, lease)
        {
        }

        protected override IWebDriver CreateDriver()
        {

            return new RemoteWebDriver(Lease.HubUri, new ChromeOptions());
        }

    }
}