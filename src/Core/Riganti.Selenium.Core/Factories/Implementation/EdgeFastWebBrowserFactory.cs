using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class EdgeFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "edge:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new EdgeFastWebBrowser(this);
        }

        public EdgeFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
