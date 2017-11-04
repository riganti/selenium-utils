using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Riganti.Selenium.Coordinator.Client
{
    public class CoordinatorClient : ClientBase
    {
        public CoordinatorClient(string apiUrl) : base(apiUrl)
        {
        }

        public Task<ContainerLeaseDataDTO> AcquireLease(string browserType)
        {
            return Call<ContainerLeaseDataDTO>("POST", "api/lease", null, new Dictionary<string, string>()
            {
                { "browserType", browserType }
            });
        }
        

        public Task<ContainerLeaseDataDTO> RenewLease(Guid leaseId)
        {
            return Call<ContainerLeaseDataDTO>("PUT", "api/lease", null, new Dictionary<string, string>()
            {
                { "leaseId", leaseId.ToString() }
            });
        }

        public Task DropLease(Guid leaseId)
        {
            return Call<ContainerLeaseDataDTO>("DELETE", "api/lease", null, new Dictionary<string, string>()
            {
                { "leaseId", leaseId.ToString() }
            });
        }
    }
}
