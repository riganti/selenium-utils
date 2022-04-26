using System;
using Riganti.Selenium.Core.Api;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class AnyOperationRunnerTests : AppSeleniumTest
    {
        [Fact]
        public void AnyOperationRunner()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                AssertUI.Any(browser.FindElements("div p")).InnerTextEquals("a");
            });

        }
        [Fact]

        public void AnyOperationRunner_FailureExpected()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("/test/FindElements");
                    AssertUI.Any(browser.FindElements("div p")).InnerTextEquals("1");
                });
            });

        }

        public AnyOperationRunnerTests(ITestOutputHelper output) : base(output)
        {
        }
    }

}
