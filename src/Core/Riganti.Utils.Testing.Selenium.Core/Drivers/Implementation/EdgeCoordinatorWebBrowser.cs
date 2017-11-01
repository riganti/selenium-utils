using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation
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
