using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class WebDriverFacotryRegistry
    {
        private static WebDriverFacotryRegistry def;
        public static WebDriverFacotryRegistry Default => def ?? (def = new WebDriverFacotryRegistry());

        public WebDriverFacotryRegistry()
        {
            BrowserFactories = new List<IWebDriverFactory>();
            if (SeleniumTestsConfiguration.StartChromeDriver) RegisterBrowserFactory(new DefaultChromeWebDriverFactory());
            if (SeleniumTestsConfiguration.StartInternetExplorerDriver) RegisterBrowserFactory(new DefaultInternetExplorerWebDriverFactory());
            if (SeleniumTestsConfiguration.StartFirefoxDriver) RegisterBrowserFactory(new DefaultFirefoxWebDriverFactory());
            if (SeleniumTestsConfiguration.AllowBrowserPooling) AllowPooling();
        }

        public WebDriverFacotryRegistry(IEnumerable<IWebDriverFactory> factories)
        {
            BrowserFactories =  factories.ToList();
        }

        public WebDriverFacotryRegistry Clone()
        {
            return new WebDriverFacotryRegistry(BrowserFactories);
        }

        public List<IWebDriverFactory> BrowserFactories { get; }

        public void RegisterBrowserFactoryMehtod(Func<IWebDriver> func)
        {
            BrowserFactories.Add(new WebDriverFactoryMethodWrapper(func));
        }

        public void AllowPooling()
        {
            for (int i = 0; i < BrowserFactories.Count; i++)
            {
                BrowserFactories[i] = new PoolingDriverFactory(BrowserFactories[i]);
            }
        }

        public void RegisterBrowserFactory(IWebDriverFactory factory)
        {
            BrowserFactories.Add(factory);
        }

        public void Clear()
        {
            BrowserFactories.Clear();
        }
    }
}