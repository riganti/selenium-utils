using System;
using OpenQA.Selenium.PhantomJS;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class PhantomHelpers
    {
        public static PhantomJSDriver CreatePhantomDriver(LocalWebBrowserFactory factory)
        {
            var options = new PhantomJSOptions();
            options.AcceptInsecureCertificates = true;

            return new PhantomJSDriver(options);
        }
    }
}
