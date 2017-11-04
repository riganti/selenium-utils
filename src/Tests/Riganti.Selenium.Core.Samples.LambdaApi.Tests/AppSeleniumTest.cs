using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.LambdaApi;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.LambdaApi.Tests
{
    public abstract class AppSeleniumTest : SeleniumTest
    {
        public void RunInAllBrowsers(Action<BrowserWrapperLambdaApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            LambdaApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }

        protected AppSeleniumTest(ITestOutputHelper output) : base(output) { }
    }
}
