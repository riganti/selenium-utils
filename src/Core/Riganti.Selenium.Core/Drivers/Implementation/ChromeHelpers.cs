using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Riganti.Selenium.Core.Factories;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class ChromeHelpers
    {
        public static ChromeDriver CreateChromeDriver(LocalWebBrowserFactory factory)
        {

            var options = new ChromeOptions();
            options.AddArgument("test-type");
            options.AddArgument("disable-popup-blocking");

            options.AddArguments(factory.Capabilities);
            options.BrowserVersion = factory.Options.TryGet(nameof(options.BrowserVersion));

            if (factory.GetBooleanOption("disableExtensions"))
            {
                options.AddArgument("--disable-extensions");
            }
            return new ChromeDriver(options);
        }
    }
}