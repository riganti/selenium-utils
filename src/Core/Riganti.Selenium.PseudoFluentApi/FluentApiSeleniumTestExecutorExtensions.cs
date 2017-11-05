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
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<IBrowserWrapperFluentApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<IBrowserWrapper, IBrowserWrapperFluentApi>();
            executor.TestSuiteRunner.RunInAllBrowsers(executor, Convert(testBody), callerMemberName, callerFilePath, callerLineNumber);
        }


        public static Action<IBrowserWrapper> Convert(Action<IBrowserWrapperFluentApi> action)
        {
            return o => action((IBrowserWrapperFluentApi)o);
        }
    }
    
}