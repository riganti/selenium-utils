using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Logging;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class OperaFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "opera:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new OperaFastWebBrowser(this);
        }

        public OperaFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
