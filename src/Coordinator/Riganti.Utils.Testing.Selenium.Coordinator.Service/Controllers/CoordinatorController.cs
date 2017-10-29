using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Data;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Services;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Service.Controllers
{
    [Route("api/lease")]
    public class CoordinatorController : Controller
    {
        private readonly ContainerLeaseRepository repository;

        public CoordinatorController(ContainerLeaseRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ContainerLeaseData> AcquireLease(string browserType)
        {
            return await repository.AcquireLease(browserType);
        }

        [HttpPut]
        public async Task<ContainerLeaseData> RenewLease(Guid leaseId)
        {
            return await repository.RenewLease(leaseId);
        }

        [HttpDelete]
        public async Task DropLease(Guid leaseId)
        {
            await repository.DropLease(leaseId);
        }

    }
    
}
