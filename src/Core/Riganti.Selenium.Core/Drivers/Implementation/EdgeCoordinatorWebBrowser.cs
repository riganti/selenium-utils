using System;
using OpenQA.Selenium;
using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class EdgeCoordinatorWebBrowser : CoordinatorWebBrowserBase
    {
        public EdgeCoordinatorWebBrowser(CoordinatorWebBrowserFactoryBase factory, ContainerLeaseDataDTO lease) : base(factory, lease)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            throw new NotImplementedException();
        }
    }
}
