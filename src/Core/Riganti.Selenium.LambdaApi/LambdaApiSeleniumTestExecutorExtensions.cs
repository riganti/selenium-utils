using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.LambdaApi
{
    public static class LambdaApiSeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<BrowserWrapperLambdaApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.RunInAllBrowsers(executor, wrapper => testBody((BrowserWrapperLambdaApi)wrapper), callerMemberName, callerFilePath, callerLineNumber);
        }

    }
}