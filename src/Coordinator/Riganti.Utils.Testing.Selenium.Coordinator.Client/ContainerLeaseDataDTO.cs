using System;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Client
{
    public class ContainerLeaseDataDTO
    {
        public Guid LeaseId { get; set; }

        public string ContainerId { get; set; }

        public DateTime ExpirationDateUtc { get; set; }

        public string Url { get; set; }
        public Uri HubUri
        {
            get
            {
                if (Url == null) throw new ArgumentException(nameof(Url));
                var builder = new UriBuilder(Url) { Path = "/wd/hub" };
                return builder.Uri;
            }
        }
    }
}