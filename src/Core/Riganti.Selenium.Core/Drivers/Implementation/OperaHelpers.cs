using System;
using OpenQA.Selenium.Opera;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class OperaHelpers
    {
        public static OperaDriver CreateOperaDriver(LocalWebBrowserFactory factory)
        {
            var options = new OperaOptions();
            options.AddArgument("test-type");
            options.AddArgument("disable-popup-blocking");

            options.AddArguments(factory.Capabilities);

            if (factory.GetBooleanOption("disableExtensions"))
            {
                options.AddArgument("--disable-extensions");
            }
            options.AcceptInsecureCertificates = true;

            return new OperaDriver(options);
        }
    }
}
