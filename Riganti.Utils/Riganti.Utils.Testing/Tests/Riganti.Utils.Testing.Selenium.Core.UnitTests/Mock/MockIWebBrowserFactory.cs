using System.Collections.Generic;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockIWebBrowserFactory : IWebBrowserFactory
    {
        public string Name => "factory";
        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();
        public TestSuiteRunner TestSuiteRunner => null;
        public Task<IWebBrowser> AcquireBrowser()
        {
            return Task.FromResult<IWebBrowser>(null);
        }

        public Task ReleaseBrowser(IWebBrowser browser)
        {
            return Task.FromResult(0);
        }
    }
}