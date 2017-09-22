using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.Core.Factories
{
    public abstract class FastLocalWebBrowserFactory : LocalWebBrowserFactory, IReusableWebBrowserFactory
    {
        public FastLocalWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }

        public void ClearBrowserState(IWebBrowser browser)
        {
            ((FastWebBrowserBase)browser).ClearDriverState();
        }
    }
}