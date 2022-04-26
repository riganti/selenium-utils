using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class AlertTests : AppSeleniumTest
    {
        public AlertTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void AlertApiStabilityTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Alert");
                var button = browser.First("input[type=button]");
                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();

                browser.NavigateToUrl("/test/Confirm");
                var message = browser.First("#message");
                button = browser.First("input[type=button]");

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.ConfirmAlert();
                AssertUI.TextEquals(message, "Accept");

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();
                AssertUI.TextEquals(message, "Dismiss");

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.ConfirmAlert();
                AssertUI.TextEquals(message, "Accept");

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.DismissAlert();
                AssertUI.TextEquals(message, "Dismiss");

                button.Click();
                browser.Wait();
                AssertUI.AlertText(browser, s => s == "confirm test");
                browser.ConfirmAlert();
                AssertUI.TextEquals(message, "Accept");
            });
        }
    }
}