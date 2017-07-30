using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
{
    public class ChromeFastWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "chrome:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new ChromeFastWebBrowser(this);
        }

        public ChromeFastWebBrowserFactory(SeleniumTestsConfiguration configuration, LoggerService loggerService, TestContextAccessor testContextAccessor) : base(configuration, loggerService, testContextAccessor)
        {
        }
    }
}