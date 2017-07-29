using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public class FirefoxCoordinatorWebBrowser : CoordinatorWebBrowserBase
    {
        public FirefoxCoordinatorWebBrowser(CoordinatorWebBrowserFactory factory, ContainerLeaseDataDTO lease) : base(factory, lease)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return new RemoteWebDriver(new Uri(Lease.Url), new FirefoxOptions());
        }

        public override void ClearBrowserState()
        {
            throw new NotImplementedException();
        }
    }
}