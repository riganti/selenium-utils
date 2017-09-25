using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests;

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
        public void AlertTextCasingTest()
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
        public void AlertContainsTest()
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
        public void AlertContainsExceptionExpectedTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextContains("confirms");
            });
        }

        [TestMethod]
        public void AlertRuleEndsWithTest()
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
        public void AlertEndsWIthExceptionExpectedTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }
  
    
    }

}