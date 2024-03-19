﻿using Riganti.Selenium.Coordinator.Client;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
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