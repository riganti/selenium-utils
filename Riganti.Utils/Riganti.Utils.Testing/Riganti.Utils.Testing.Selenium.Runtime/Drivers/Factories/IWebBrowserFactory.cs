using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories
{
    public interface IWebBrowserFactory
    {

        IReadOnlyDictionary<string, string> Options { get; }

        Task<IWebBrowser> AcquireBrowser();

        Task ReleaseBrowser(IWebBrowser browser);

    }
}