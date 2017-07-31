using System;
using System.Diagnostics;
using Riganti.Utils.Testing.Selenium.Core.Factories;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public static class LoggingExtensions
    {

        public static void LogVerbose(this IWebBrowserFactory factory, string message)
        {
            factory.TestSuiteRunner.LogVerbose(message);
        }

        public static void LogInfo(this IWebBrowserFactory factory, string message)
        {
            factory.TestSuiteRunner.LogInfo(message);
        }

        public static void LogError(this IWebBrowserFactory factory, Exception ex)
        {
            factory.TestSuiteRunner.LogError(ex);
        }


        public static void LogVerbose(this TestSuiteRunner runner, string message)
        {
            runner.LoggerService.WriteLine(runner.TestContextAccessor.GetTestContext(), message, TraceLevel.Verbose);
        }

        public static void LogInfo(this TestSuiteRunner runner, string message)
        {
            runner.LoggerService.WriteLine(runner.TestContextAccessor.GetTestContext(), message, TraceLevel.Info);
        }

        public static void LogError(this TestSuiteRunner runner, Exception ex)
        {
            runner.LoggerService.WriteLine(runner.TestContextAccessor.GetTestContext(), ex.ToString(), TraceLevel.Error);
        }

    }
}
