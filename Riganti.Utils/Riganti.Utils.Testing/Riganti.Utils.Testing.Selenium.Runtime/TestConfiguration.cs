using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public class TestConfiguration
    {

        public IWebBrowserFactory Factory { get; set; }

        public string BaseUrl { get; set; }

    }
}