using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories
{
    public abstract class LocalWebBrowserFactory : IWebBrowserFactory
    {
        
        public abstract string Name { get; }

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();
        

        public SeleniumTestsConfiguration Configuration { get; }

        public LoggerService LoggerService { get; }

        public TestContextAccessor TestContextAccessor { get; }



        public LocalWebBrowserFactory(SeleniumTestsConfiguration configuration, LoggerService loggerService, TestContextAccessor testContextAccessor)
        {
            Configuration = configuration;
            LoggerService = loggerService;
            TestContextAccessor = testContextAccessor;
        }


        public Task<IWebBrowser> AcquireBrowser()
        {
            return Task.FromResult(CreateBrowser());
        }

        public Task ReleaseBrowser(IWebBrowser browser)
        {
            DisposeBrowser(browser);
            return Task.FromResult(0);
        }



        protected abstract IWebBrowser CreateBrowser();

        protected virtual void DisposeBrowser(IWebBrowser browser)
        {
            browser.Dispose();
        }
    }
}
