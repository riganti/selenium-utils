using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class DefaultInternetExplorerWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            InternetExplorerOptions options = new InternetExplorerOptions
            {
                BrowserCommandLineArguments = "-private",
                //IgnoreZoomLevel =  false,
                //EnableNativeEvents = true, 
                //RequireWindowFocus = true,
            };
            var driver = new InternetExplorerDriverWrapper(options);
            driver.SetDefaultTimeouts();
            return driver;
        }
    }
}