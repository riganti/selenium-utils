using System;
using System.Diagnostics;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core
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


        public static void LogVerbose(this TestSuiteRunner runner, string message) {
            runner.LoggerService.WriteLine(runner.TestContextProvider.GetGlobalScopeTestContext(), message, TraceLevel.Verbose);
        }

        public static void LogInfo(this TestSuiteRunner runner, string message) 
        {
            runner.LoggerService.WriteLine(runner.TestContextProvider.GetGlobalScopeTestContext(), message, TraceLevel.Info);
        }

        public static void LogError(this TestSuiteRunner runner, Exception ex) 
        {
            runner.LoggerService.WriteLine(runner.TestContextProvider.GetGlobalScopeTestContext(), ex.ToString(), TraceLevel.Error);
        }

    }
}
