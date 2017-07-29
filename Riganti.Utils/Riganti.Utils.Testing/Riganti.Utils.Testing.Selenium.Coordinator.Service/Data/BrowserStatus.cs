using System;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Service.Data
{
    public class BrowserStatus
    {

        public string ContainerId { get; set; }

        public string BrowserType { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime? ExpirationDateUtc { get; set; }

        public string Url { get; set; }

    }
}