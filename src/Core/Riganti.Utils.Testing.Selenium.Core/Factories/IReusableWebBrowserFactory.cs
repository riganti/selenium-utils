using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.Core.Factories
{
    public interface IReusableWebBrowserFactory : IWebBrowserFactory
    {

        void ClearBrowserState(IWebBrowser browser);

    }
}