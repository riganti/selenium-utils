using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class WebDriverFacotryRegistry
    {
        public WebDriverFacotryRegistry()
        {
            BrowserFactories = new List<IWebDriverFactory>();
            if (SeleniumTestsConfiguration.StartChromeDriver) RegisterBrowserFactory(new DefaultChromeWebDriverFactory());
            if (SeleniumTestsConfiguration.StartInternetExplorerDriver) RegisterBrowserFactory(new DefaultInternetExplorerWebDriverFactory());
            if (SeleniumTestsConfiguration.StartFirefoxDriver) RegisterBrowserFactory(new DefaultFirefoxWebDriverFactory());
        }

        public List<IWebDriverFactory> BrowserFactories { get; }

        public void RegisterBrowserFactoryMehtod(Func<IWebDriver> func)
        {
            BrowserFactories.Add(new WebDriverFactoryMethodWrapper(func));
        }

        public void RegisterBrowserFactory(IWebDriverFactory factory)
        {
            BrowserFactories.Add(factory);
        }

        public void Clear(IWebDriverFactory factory)
        {
            BrowserFactories.Clear();
        }
    }
}