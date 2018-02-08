using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Logging;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    class OperaDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "opera:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new OperaDevWebBrowser(this);
        }

        public OperaDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
