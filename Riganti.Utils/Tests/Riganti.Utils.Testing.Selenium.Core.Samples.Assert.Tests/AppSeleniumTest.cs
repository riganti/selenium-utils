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
        public void RunInAllBrowsers(Action<BrowserWrapperAsertApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            TestSuiteRunner.RunInAllBrowsers(this, o => testBody((BrowserWrapperAsertApi)o), callerMemberName, callerFilePath, callerLineNumber);
        }
        public AppSeleniumTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}
