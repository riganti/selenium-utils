using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class AlertTests : SeleniumTest
    {
        [TestMethod]
        public void AlertTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("confirm test");
            });
        }

        [TestMethod]
        public void AlertTest2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                try
                {
                    browser.CheckIfAlertTextEquals("Confirm test", true);
                }
                catch (AlertException)
                {
                }
            });
        }

        [TestMethod]
        public void AlertTest3()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextContains("confirm");
            });
        }

        [TestMethod]
        public void AlertTest4()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test"), "alert text doesn't end with 'test.'");
            });
        }
    }
}