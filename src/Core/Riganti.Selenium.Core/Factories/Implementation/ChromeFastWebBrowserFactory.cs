using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
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