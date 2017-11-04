using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.Core.Factories
{
    public interface IReusableWebBrowserFactory : IWebBrowserFactory
    {

        void ClearBrowserState(IWebBrowser browser);

    }
}