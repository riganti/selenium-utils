using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Factories
{
    public class LocalWebBrowserFactory : IWebBrowserFactory
    {

        private readonly Dictionary<string, Func<IWebBrowser>> browserFactories;

        public LocalWebBrowserFactory()
        {
            browserFactories = new Dictionary<string, Func<IWebBrowser>>()
            {
                { "chrome:fast", () => new ChromeFastWebBrowser(this) },
                { "chrome:dev", () => new ChromeDevWebBrowser(this) },
                { "firefox:fast", () => new FirefoxDevWebBrowser(this) },
                { "firefox:dev", () => new FirefoxFastWebBrowser(this) },
                { "ie:fast", () => new InternetExplorerFastWebBrowser(this) },
                { "ie:dev", () => new InternetExplorerDevWebBrowser(this) }
            };
        }

        public IReadOnlyDictionary<string, string> Options { get; internal set; }

        public string BrowserType { get; set; }


        public Task<IWebBrowser> AcquireBrowser()
        {
            return Task.FromResult(CreateBrowser());
        }

        public Task ReleaseBrowser(IWebBrowser browser)
        {
            DisposeBrowser(browser);
            return Task.FromResult(0);
        }



        protected virtual IWebBrowser CreateBrowser()
        {
            return browserFactories[BrowserType]();
        }

        protected virtual void DisposeBrowser(IWebBrowser browser)
        {
            browser.Dispose();
        }
    }
}
