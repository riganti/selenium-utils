using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories
{
    public class CoordinatorWebBrowserFactory : IWebBrowserFactory
    {
        private readonly Dictionary<string, Func<ContainerLeaseDataDTO, CoordinatorWebBrowserBase>> browserFactories;
        private readonly Timer timer;
        
        private readonly List<CoordinatorWebBrowserBase> createdBrowsers = new List<CoordinatorWebBrowserBase>();
        private readonly object createdBrowsersLocker = new object();

        
        private CoordinatorClient client;
        protected CoordinatorClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new CoordinatorClient(CoordinatorUrl);
                }
                return client;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the coordinator service.
        /// </summary>
        public string CoordinatorUrl { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the browser. Use 'chrome' or 'firefox'.
        /// </summary>
        public string BrowserType { get; set; }


        public CoordinatorWebBrowserFactory()
        {
            browserFactories = new Dictionary<string, Func<ContainerLeaseDataDTO, CoordinatorWebBrowserBase>>()
            {
                { "chrome", lease => new ChromeCoordinatorWebBrowser(this, lease) },
                { "firefox", lease => new FirefoxCoordinatorWebBrowser(this, lease) }
            };

            timer = new Timer(RenewLeases, null, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(15));
        }


        public IReadOnlyDictionary<string, string> Options { get; internal set; }

        public async Task<IWebBrowser> AcquireBrowser()
        {
            ContainerLeaseDataDTO lease;
            while (true)
            {
                lease = await Client.AcquireLease(BrowserType);
                if (lease != null)
                {
                    return CreateBrowser(lease);
                }

                await Task.Delay(5000);
            }
        }

        private IWebBrowser CreateBrowser(ContainerLeaseDataDTO lease)
        {
            var createdBrowser = CreateBrowserCore(lease);
            lock (createdBrowsersLocker)
            {
                createdBrowsers.Add(createdBrowser);
            }
            return createdBrowser;
        }

        protected virtual CoordinatorWebBrowserBase CreateBrowserCore(ContainerLeaseDataDTO lease)
        {
            return browserFactories[BrowserType](lease);
        }


        public async Task ReleaseBrowser(IWebBrowser browser)
        {
            var coordinatorWebBrowser = (CoordinatorWebBrowserBase) browser;
            lock (createdBrowsersLocker)
            {
                createdBrowsers.Remove(coordinatorWebBrowser);
            }
            await Client.DropLease(coordinatorWebBrowser.Lease.LeaseId);
        }

        private void RenewLeases(object state)
        {
            List<CoordinatorWebBrowserBase> createdBrowsersCopy;
            lock (createdBrowsersLocker)
            {
                createdBrowsersCopy = new List<CoordinatorWebBrowserBase>(createdBrowsers);
            }

            foreach (var browser in createdBrowsersCopy)
            {
                Client.RenewLease(browser.Lease.LeaseId).Wait();
            }
        }

    }
}
