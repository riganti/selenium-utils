using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Riganti.Selenium.Coordinator.Service.Data;
using Riganti.Selenium.Coordinator.Service.Hubs;

namespace Riganti.Selenium.Coordinator.Service.Services
{
    public class ContainerLeaseRepository : IDisposable
    {

        private readonly IOptions<AppConfiguration> options;
        private readonly DockerProvisioningService dockerProvisioningService;
        private readonly ILogger<ContainerLeaseRepository> logger;
        private readonly IHubContext<LogHub> hubContext;
        private readonly Timer timer;


        private readonly Dictionary<string, List<ContainerLeaseData>> leases;
        private readonly object locker = new object();


        private static readonly TimeSpan LeaseTimeout = TimeSpan.FromMinutes(1);


        public ContainerLeaseRepository(IOptions<AppConfiguration> options, DockerProvisioningService dockerProvisioningService, ILogger<ContainerLeaseRepository> logger, IHubContext<LogHub> hubContext)
        {
            this.options = options;
            this.dockerProvisioningService = dockerProvisioningService;
            this.logger = logger;
            this.hubContext = hubContext;

            this.timer = new Timer(DropExpiredLocks, null, LeaseTimeout, LeaseTimeout);
            
            lock (locker)
            {
                leases = options.Value.Browsers.ToDictionary(b => b.BrowserType, b => Enumerable.Range(0, b.MaxInstances).Select(i => (ContainerLeaseData) null).ToList());
            }
        }



        public async Task<ContainerLeaseData> AcquireLease(string browserType)
        {
            logger.LogInformation($"Acquiring lock for browser {browserType}...");

            int index;
            ContainerLeaseData lease;

            lock (locker)
            {
                var browserLeases = leases[browserType];

                // find unused instance
                index = browserLeases.FindIndex(l => l == null);
                if (index < 0)
                {
                    logger.LogWarning($"No containers for browser {browserType} are available right now. Try again later.");
                    return null;
                }

                // create lease
                lease = new ContainerLeaseData()
                {
                    LeaseId = Guid.NewGuid(),
                    ExpirationDateUtc = DateTime.UtcNow + LeaseTimeout
                };
                browserLeases[index] = lease;
                logger.LogDebug($"Acquired lease {lease.LeaseId}, preparing container...");
            }

            try
            {
                // start container
                var container = await dockerProvisioningService.StartContainer(options.Value.GetBrowser(browserType).ImageName, browserType, index);

                // return lease
                lease.ContainerId = container.ContainerId;
                lease.Url = container.Url;
                lease.ExpirationDateUtc = DateTime.UtcNow + LeaseTimeout;
                logger.LogInformation($"Lock for browser {browserType} (ID={lease.LeaseId}) acquired and valid until {lease.ExpirationDateUtc}.");

                LogHub.Refresh(hubContext);
                
                return lease;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error acquiring lock for browser {browserType}!");
                throw;
            }

        }

        public Task<ContainerLeaseData> RenewLease(Guid leaseId)
        {
            logger.LogInformation($"Renewing lock {leaseId}...");

            try
            {
                lock (locker)
                {
                    // find lease
                    var lease = leases.SelectMany(l => l.Value).FirstOrDefault(l => l?.LeaseId == leaseId);
                    if (lease == null)
                    {
                        throw new KeyNotFoundException($"The lease with ID={leaseId} does not exist!");
                    }

                    // renew
                    lease.ExpirationDateUtc = DateTime.UtcNow + LeaseTimeout;

                    logger.LogInformation($"Lock ID={lease.LeaseId} renewed and valid until {lease.ExpirationDateUtc}.");

                    LogHub.Refresh(hubContext);

                    return Task.FromResult(lease);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error renewing lock {leaseId}!");
                throw;
            }
        }

        public async Task DropLease(Guid leaseId)
        {
            ContainerLeaseData lease = null;

            lock (locker)
            {
                // find lease
                foreach (var browser in leases)
                {
                    var index = browser.Value.FindIndex(l => l?.LeaseId == leaseId);
                    if (index >= 0)
                    {
                        lease = browser.Value[index];
                        
                        // drop lease
                        browser.Value[index] = null;
                        break;
                    }
                }
            }

            if (lease != null)
            {
                // stop container
                await dockerProvisioningService.StopContainer(lease.ContainerId);
                logger.LogInformation($"The lease {leaseId} was dropped and the container was stopped.");
            }
            else
            {
                logger.LogInformation($"The lease {leaseId} has already been dropped, no action was taken.");
            }

            LogHub.Refresh(hubContext);
        }

        private void DropExpiredLocks(object state)
        {
            // find expired leases
            List<ContainerLeaseData> leasesToDrop;
            lock (locker)
            {
                leasesToDrop = leases.SelectMany(l => l.Value).Where(l => l?.ExpirationDateUtc < DateTime.UtcNow).ToList();
            }

            // drop locks
            foreach (var lease in leasesToDrop)
            {
                try
                {
                    logger.LogInformation($"The lease {lease.LeaseId} has expired, dropping...");
                    DropLease(lease.LeaseId).Wait();
                }
                catch
                {
                }
            }
        }

        public List<BrowserStatus> GetAllBrowsers()
        {
            lock (locker)
            {
                // find lease
                return leases
                    .SelectMany(b => b.Value.Select(l => new { BrowserType = b.Key, Lease = l }))
                    .Select(l => new BrowserStatus()
                    {
                        BrowserType = l.BrowserType,
                        IsAvailable = l.Lease == null,
                        ContainerId = l.Lease?.ContainerId,
                        Url = l.Lease?.Url,
                        ExpirationDateUtc = l.Lease?.ExpirationDateUtc
                    })
                    .ToList();
            }
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
