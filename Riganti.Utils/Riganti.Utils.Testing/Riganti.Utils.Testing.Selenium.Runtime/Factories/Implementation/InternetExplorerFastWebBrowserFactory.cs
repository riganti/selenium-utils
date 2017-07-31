using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
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