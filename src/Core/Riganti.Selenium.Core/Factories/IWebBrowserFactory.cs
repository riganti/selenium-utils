using System.Collections.Generic;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.Core.Factories
{
    public interface IWebBrowserFactory
    {
        string Name { get; }

        IDictionary<string, string> Options { get; }

        IList<string> Capabilities { get; }
        
        TestSuiteRunner TestSuiteRunner { get; }

        Task<IWebBrowser> AcquireBrowser();
        
        Task ReleaseBrowser(IWebBrowser browser);

    }
}