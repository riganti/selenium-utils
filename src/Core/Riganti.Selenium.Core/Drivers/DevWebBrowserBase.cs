using Riganti.Selenium.Core.Factories;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core.Drivers
{
    public abstract class DevWebBrowserBase : WebBrowserBase
    {
        public DevWebBrowserBase(IWebBrowserFactory factory) : base(factory)
        {
        }
        
    }
}