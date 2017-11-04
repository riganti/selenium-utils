using OpenQA.Selenium.IE;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class InternetExplorerHelpers
    {

        public static InternetExplorerDriver CreateInternetExplorerDriver(LocalWebBrowserFactory factory)
        {
            var options = new InternetExplorerOptions
            {
                BrowserCommandLineArguments = "-private"
            };

            return new InternetExplorerDriver(options);
        }

    }
}