using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
{
    public class InternetExplorerDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "ie:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new InternetExplorerDevWebBrowser(this);
        }

        public InternetExplorerDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}