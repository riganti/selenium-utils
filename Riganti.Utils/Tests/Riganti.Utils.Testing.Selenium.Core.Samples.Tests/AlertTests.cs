using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests
{
    [TestClass]
    public class AlertTests : SeleniumTest
    {
        [TestMethod]
        public void Alert_CheckIfAlertTextEquals()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("confirm test");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(AlertException))]
        public void Alert_CheckIfAlertTextEquals_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(AlertException))]
        public void Alert_CheckIfAlertTextEquals_CaseSensitive_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("Confirm test", true);
            });
        }

        [TestMethod]
        public void Alert_CheckIfAlertTextContains()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextContains("confirm");
            });
        }
        [TestMethod]
        [ExpectedSeleniumException(typeof(AlertException))]
        public void Alert_CheckIfAlertTextContains_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextContains("confirms");
            });
        }

        [TestMethod]
        public void Alert_CheckIfAlertText()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test"), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(AlertException))]
        public void Alert_CheckIfAlertText_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void Alert_ConfirmAlert()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Confirm");

                var button = browser.First("#button");
                button.Click();

                browser.ConfirmAlert().First("#message").CheckIfInnerTextEquals("Accept", false);

                button.Click();
                browser.DismissAlert().First("#message").CheckIfInnerTextEquals("Dismiss", false);
            });
        }
    }
}