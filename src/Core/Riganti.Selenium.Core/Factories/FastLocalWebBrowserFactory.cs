using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.Core.Factories
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