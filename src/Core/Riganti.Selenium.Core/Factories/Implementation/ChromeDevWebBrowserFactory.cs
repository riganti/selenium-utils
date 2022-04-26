using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class ChromeDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "chrome:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new ChromeDevWebBrowser(this);
        }

        public ChromeDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
