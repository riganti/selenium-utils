using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
{
    public class FirefoxDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "firefox:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new FirefoxDevWebBrowser(this);
        }

        public FirefoxDevWebBrowserFactory(SeleniumTestsConfiguration configuration, LoggerService loggerService, TestContextAccessor testContextAccessor) : base(configuration, loggerService, testContextAccessor)
        {
        }
    }
}