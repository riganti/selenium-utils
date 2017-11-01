using Riganti.Utils.Testing.Selenium.Core.Factories;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers
{
    public abstract class DevWebBrowserBase : WebBrowserBase
    {
        public DevWebBrowserBase(IWebBrowserFactory factory) : base(factory)
        {
        }
        
    }
}