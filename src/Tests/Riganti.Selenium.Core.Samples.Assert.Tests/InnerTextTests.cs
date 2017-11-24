using Riganti.Selenium.AssertApi;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class InnerTextTests : AppSeleniumTest
    {
        public InnerTextTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                AssertUI.AlertTextEquals(browser, "confirm test");

            });
        }
    }

}