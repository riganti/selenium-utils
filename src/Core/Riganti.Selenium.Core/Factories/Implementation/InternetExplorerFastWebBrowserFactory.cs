using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Logging;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class InternetExplorerFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "ie:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new InternetExplorerFastWebBrowser(this);
        }

        public InternetExplorerFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}