using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class BrowsersStabilityUiTests : SeleniumTest
    {
        [TestMethod]
        public void BrowserStability_ButtonClick()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ClickTest");

                var button = browser.First("button");
                var input = browser.First("#testInput");

                //check init
                input.CheckIfValue("0");

                //click and check if the driver clicked only once
                button.Click();
                input.CheckIfValue("1");

                //click and check if the driver clicked only once
                button.Click();
                input.CheckIfValue("2");

                //click and check if the driver clicked only once
                button.Click();
                button.Click();
                input.CheckIfValue("4");

                //click and check if the driver clicked only once
                button.Click();
                button.Click();
                button.Click();
                button.Click();
                button.Click();
                button.Click();
                button.Click();
                button.Click();
                button.Click();
                input.CheckIfValue("13");
            });
        }

        [TestMethod]
        public void SelectMethod_XPath()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ClickTest");
                var elem = browser.Single("#span");
                elem.Single("..", By.XPath);
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(EmptySequenceException))]
        public void SelectMethod_XPathToRoot_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ClickTest");
                browser.Single("..", By.XPath);
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(JavaScriptException))]
        public void SelectMethod_InvalidXPathSelector_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ClickTest");
                var elem = browser.Single("#span");
                elem.Single("///***-*///@@##@šš+š++++---><<>''", By.XPath);
            });
        }
    }
}