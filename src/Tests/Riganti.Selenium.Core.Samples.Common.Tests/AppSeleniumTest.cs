using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core.Abstractions;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public abstract class AppSeleniumTest : SeleniumTest
    {
        public AppSeleniumTest(ITestOutputHelper output) : base(output)
        {
        }

        protected void RunInAllBrowsers(Action<IBrowserWrapper> testBody, [CallerMemberName]string callerMemberName = "",
            [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            AssertApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}