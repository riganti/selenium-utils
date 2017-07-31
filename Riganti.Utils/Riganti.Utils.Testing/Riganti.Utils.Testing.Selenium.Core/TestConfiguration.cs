using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TestConfiguration
    {

        public IWebBrowserFactory Factory { get; set; }

        public string BaseUrl { get; set; }

    }
}