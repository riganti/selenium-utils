namespace Riganti.Selenium.Coordinator.Service.Model
{
    public class BrowserStatusDTO
    {

        public bool IsAvailable => Status == "Running";

        public string Status { get; set; }

        public string Id { get; set; }

        public string Browser { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
