using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
    public abstract class DevWebBrowserBase : WebBrowserBase
    {
        public DevWebBrowserBase(IWebBrowserFactory factory) : base(factory)
        {
        }
        
    }
}