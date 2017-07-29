using System.Linq;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Data;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Service
{
    public class AppConfiguration
    {

        public BrowserContainerConfiguration[] Browsers { get; set; }

        public string DockerApiUrl { get; set; }
        public string ExternalUrlPattern { get; set; }



        public BrowserContainerConfiguration GetBrowser(string browserType)
        {
            return Browsers.FirstOrDefault(b => b.BrowserType == browserType);
        }
    }
}