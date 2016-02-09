using System;
using System.Collections.Generic;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class FastModeWebDriverFactoryRegistry
    {
        public List<IWebDriverFactory> BrowserFactories { get; }

        public FastModeWebDriverFactoryRegistry()
        {
            BrowserFactories = new List<IWebDriverFactory>();
            if (SeleniumTestsConfiguration.FastMode)
            {
                if (SeleniumTestsConfiguration.StartChromeDriver) RegisterBrowserFactory(new ChromeFastModeFactoryBase());
                if (SeleniumTestsConfiguration.StartInternetExplorerDriver) RegisterBrowserFactory(new InternetExplorerFastModeFactoryBase());
                if (SeleniumTestsConfiguration.StartFirefoxDriver) RegisterBrowserFactory(new FirefoxFastModeFactoryBase());
            }
        }


        public void RegisterBrowserFactoryMethod(Func<ISelfCleanUpWebDriver> func)
        {
            BrowserFactories.Add(new FastModeWebDriverFactoryMethodWrapper(func));
        }

        public void RegisterBrowserFactory(IFastModeFactory factory)
        {
            if (!SeleniumTestsConfiguration.FastMode) throw new Exception("Registration of FastMode factories is not allowed when the SeleniumTestsConfiguration.FastMode is false.");
            BrowserFactories.Add(factory);
        }

        ~FastModeWebDriverFactoryRegistry()
        {
            Dispose();
        }

        public void Dispose()
        {

            BrowserFactories.ForEach(b => (b as IFastModeFactory)?.Dispose());
        }
    }
}