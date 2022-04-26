using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers
{
    public abstract class DevWebBrowserBase : WebBrowserBase
    {
        public DevWebBrowserBase(IWebBrowserFactory factory) : base(factory)
        {
        }
        
    }
}