﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class FirefoxCoordinatorWebBrowser : CoordinatorWebBrowserBase
    {
        public FirefoxCoordinatorWebBrowser(CoordinatorWebBrowserFactoryBase factory, ContainerLeaseDataDTO lease) : base(factory, lease)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            return new RemoteWebDriver( Lease.HubUri, new FirefoxOptions());
        }
        
    }
}