using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class PhantomFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "phantom:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new PhantomFastWebBrowser(this);
        }

        public PhantomFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
