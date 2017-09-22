using System;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Client
{
    public class ContainerLeaseDataDTO
    {
        public Guid LeaseId { get; set; }

        public string ContainerId { get; set; }

        public DateTime ExpirationDateUtc { get; set; }

        public string Url { get; set; }
    }
}