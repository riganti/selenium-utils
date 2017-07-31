using Riganti.Utils.Testing.Selenium.Runtime.Drivers;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories
{
    public interface IReusableWebBrowserFactory : IWebBrowserFactory
    {

        void ClearBrowserState(IWebBrowser browser);

    }
}