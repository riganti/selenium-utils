using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories
{
    public abstract class CoordinatorWebBrowserFactoryBase : IWebBrowserFactory
    {
        private readonly Timer timer;
        
        private readonly List<CoordinatorWebBrowserBase> createdBrowsers = new List<CoordinatorWebBrowserBase>();
        private readonly object createdBrowsersLocker = new object();


        public abstract string Name { get; }

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();


        public SeleniumTestsConfiguration Configuration { get; }

        public LoggerService LoggerService { get; }

        public TestContextAccessor TestContextAccessor { get; }



        public CoordinatorWebBrowserFactoryBase(SeleniumTestsConfiguration configuration, LoggerService loggerService, TestContextAccessor testContextAccessor)
        {
            Configuration = configuration;
            LoggerService = loggerService;
            TestContextAccessor = testContextAccessor;

            timer = new Timer(RenewLeases, null, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(15));
        }



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

        protected string CoordinatorUrl => Options[nameof(CoordinatorUrl)];

        protected abstract string BrowserType { get; }
        

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

        protected abstract CoordinatorWebBrowserBase CreateBrowserCore(ContainerLeaseDataDTO lease);


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
