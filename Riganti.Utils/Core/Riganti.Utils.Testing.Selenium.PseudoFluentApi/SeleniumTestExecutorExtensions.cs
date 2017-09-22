using System;
using System.Runtime.CompilerServices;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public static class SeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<BrowserWrapperPseudoFluentApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<BrowserWrapper, BrowserWrapperPseudoFluentApi>();
            executor.TestSuiteRunner.RunInAllBrowsers(executor, Convert(testBody), callerMemberName, callerFilePath, callerLineNumber);
        }

        public static Action<IBrowserWrapper> Convert(Action<BrowserWrapperPseudoFluentApi> action)
        {
            return o => action((BrowserWrapperPseudoFluentApi)o);
        }
    }
}