using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
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