using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public static class FluentApiSeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<BrowserWrapperFluentApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<BrowserWrapper, BrowserWrapperFluentApi>();
            executor.TestSuiteRunner.RunInAllBrowsers(executor, Convert(testBody), callerMemberName, callerFilePath, callerLineNumber);
        }


        public static Action<IBrowserWrapper> Convert(Action<BrowserWrapperFluentApi> action)
        {
            return o => action((BrowserWrapperFluentApi)o);
        }
    }
    
}