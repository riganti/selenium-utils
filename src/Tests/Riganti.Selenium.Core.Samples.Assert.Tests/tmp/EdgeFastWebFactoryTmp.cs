using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;
using Riganti.Selenium.Core.Factories;
using Riganti.Selenium.Core.Factories.Implementation;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests.tmp
{
    public class EdgeFastWebBrowserTmp : EdgeFastWebBrowser
    {
        public EdgeFastWebBrowserTmp(IWebBrowserFactory factory) : base(factory)
        {
        }

        protected override void DeleteAllCookies()
        {
            //ignore
        }
    }

    public class EdgeFastWebFactoryTmp : EdgeFastWebBrowserFactory
    {
        public EdgeFastWebFactoryTmp(TestSuiteRunner runner) : base(runner)
        {
        }

        public override string Name { get; } = "edgeTmp:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new EdgeFastWebBrowserTmp(this);
        }
    }
}
