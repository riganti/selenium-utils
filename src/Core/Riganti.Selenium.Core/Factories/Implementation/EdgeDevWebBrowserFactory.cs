using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class EdgeDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "edge:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new EdgeDevWebBrowser(this);
        }

        public EdgeDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
