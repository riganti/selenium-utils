using System;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Service.Data
{
    public class ContainerLeaseData
    {
        public Guid LeaseId { get; set; }

        public string ContainerId { get; set; }

        public DateTime ExpirationDateUtc { get; set; }

        public string Url { get; set; }

    }
}