using System;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.AssertApi;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.AssertApi.Tests
{
    public abstract class AppSeleniumTest : SeleniumTest
    {
        public void RunInAllBrowsers(Action<BrowserWrapperAssertApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            AssertApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }
        public AppSeleniumTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}
