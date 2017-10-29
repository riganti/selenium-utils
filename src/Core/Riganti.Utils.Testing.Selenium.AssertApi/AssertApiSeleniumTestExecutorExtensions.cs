using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.AssertApi
{
    public static class AssertApiSeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<BrowserWrapperAssertApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.ServiceFactory.RegisterTransient<BrowserWrapper, BrowserWrapperAssertApi>();
            executor.TestSuiteRunner.RunInAllBrowsers(executor, o => testBody((BrowserWrapperAssertApi)o), callerMemberName, callerFilePath, callerLineNumber);
        }

    }
}
