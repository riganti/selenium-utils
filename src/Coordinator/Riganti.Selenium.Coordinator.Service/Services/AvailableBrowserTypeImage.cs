namespace Riganti.Selenium.Coordinator.Service.Services
{
    public class AvailableBrowserTypeImage
    {
        public string BrowserType { get; set; }

        public string ImageName { get; set; }

        public int MaxInstances { get; set; }
        public bool IsAvailable { get; set; }
    }
}