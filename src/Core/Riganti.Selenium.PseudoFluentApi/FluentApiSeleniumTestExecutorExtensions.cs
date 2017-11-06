using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.FluentApi;

namespace Riganti.Selenium.Core
{
    public static class FluentApiSeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<IBrowserWrapperFluentApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<IBrowserWrapper, BrowserWrapperFluentApi>();
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<IElementWrapper, ElementWrapperFluentApi>();
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<IElementWrapperCollection, ElementWrapperCollectionFluetApi>();
            executor.TestSuiteRunner.RunInAllBrowsers(executor, Convert(testBody), callerMemberName, callerFilePath, callerLineNumber);
        }


        public static Action<IBrowserWrapper> Convert(Action<IBrowserWrapperFluentApi> action)
        {
            return o => action((IBrowserWrapperFluentApi)o);
        }
    }
    
}