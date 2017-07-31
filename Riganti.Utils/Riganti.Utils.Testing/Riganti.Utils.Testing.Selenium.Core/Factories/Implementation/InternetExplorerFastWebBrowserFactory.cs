using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
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