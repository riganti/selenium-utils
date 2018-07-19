using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Attributes;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class SkipBrowserAttributeTest : SeleniumTest
    {
        public SkipBrowserAttributeTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [SkipBrowser("chrome:fast", "Skips this test to prove this attribute works")]
        public void ThrowExceptionInTest_ExpectedSkippedTest()
        {
            RunInAllBrowsers(_ => throw new Exception("This exception will be skipped"));
        }

        private void RunInAllBrowsers(Action<IBrowserWrapper> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            AssertApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}