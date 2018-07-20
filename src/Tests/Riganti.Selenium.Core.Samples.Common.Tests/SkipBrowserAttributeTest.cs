using System;
using Riganti.Selenium.Core.Abstractions.Attributes;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class SkipBrowserAttributeTest : AppSeleniumTest
    {
        public SkipBrowserAttributeTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [SkipBrowser("chrome:fast", "Skip this test to prove this attribute works")]
        public void ThrowExceptionInTest_ExpectedSkippedTest()
        {
            RunInAllBrowsers(_ => throw new Exception("This exception will be skipped"));
        }
    }
}