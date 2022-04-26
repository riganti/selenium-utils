using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.AssertApi
{
    public static class AssertApiSeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<IBrowserWrapper> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<IBrowserWrapper, BrowserWrapper>();
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<IElementWrapper, ElementWrapper>();
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<ISeleniumWrapperCollection, ElementWrapperCollection<IElementWrapper, IBrowserWrapper>>();
            executor.TestSuiteRunner.RunInAllBrowsers(executor, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }

    }
}
