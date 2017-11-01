using System;
using System.Runtime.CompilerServices;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.LambdaApi;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.LambdaApi.Tests
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
