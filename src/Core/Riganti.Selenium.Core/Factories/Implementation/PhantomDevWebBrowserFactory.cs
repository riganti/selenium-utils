using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Riganti.Selenium.Core.Factories.Implementation
{
    public class PhantomDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "phantom:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new PhantomDevWebBrowser(this);
        }

        public PhantomDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
