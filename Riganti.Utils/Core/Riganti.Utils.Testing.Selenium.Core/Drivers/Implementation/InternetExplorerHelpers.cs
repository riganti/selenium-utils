using OpenQA.Selenium.IE;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation
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