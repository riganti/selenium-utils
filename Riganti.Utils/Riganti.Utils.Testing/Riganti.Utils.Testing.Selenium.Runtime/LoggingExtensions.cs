using System;
using System.Diagnostics;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public static class LoggingExtensions
    {

        public static void LogMessage(this IWebBrowserFactory factory, string message)
        {
            factory.LoggerService.WriteLine(factory.TestContextAccessor.GetTestContext(), message, TraceLevel.Info);
        }

        public static void LogError(this IWebBrowserFactory factory, Exception ex)
        {
            factory.LoggerService.WriteLine(factory.TestContextAccessor.GetTestContext(), ex.ToString(), TraceLevel.Error);
        }

    }
}
