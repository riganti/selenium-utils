using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
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