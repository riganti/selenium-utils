using System;
using System.Runtime.CompilerServices;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    public class AppSeleniumTest : SeleniumTest
    {

        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public void RunInAllBrowsers(Action<IBrowserWrapperFluentApi> testBody,
            [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            FluentApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }

    }
}