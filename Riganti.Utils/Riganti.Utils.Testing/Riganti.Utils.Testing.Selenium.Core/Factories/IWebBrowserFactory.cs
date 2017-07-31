using System.Collections.Generic;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core.Factories
{
    public interface IWebBrowserFactory
    {
        string Name { get; }

        IDictionary<string, string> Options { get; }
        
        TestSuiteRunner TestSuiteRunner { get; }

        Task<IWebBrowser> AcquireBrowser();
        
        Task ReleaseBrowser(IWebBrowser browser);

    }
}