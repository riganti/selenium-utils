using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
{
    public class ChromeFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "chrome:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new ChromeFastWebBrowser(this);
        }

        public ChromeFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}