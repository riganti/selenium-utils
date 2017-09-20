using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Api;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class BrowsersStabilityUiTestsNewApi : SeleniumTest
    {
        [TestMethod]
        public void ButtonClickTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/ClickTest.aspx");

                var button = browser.First("button");
                var input = browser.First("#testInput");

                //check init
                AssertUI.CheckIfValue(input, "0");

                //click and check if the driver clicked only once
                button.Click();
                AssertUI.CheckIfValue(input, "1");

                //click and check if the driver clicked only once
                button.Click();
                AssertUI.CheckIfValue(input, "2");

                //click and check if the driver clicked only once
                button.Click();
                button.Click();
                AssertUI.CheckIfValue(input, "4");

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
                AssertUI.CheckIfValue(input, "13");
            });


        }

        [TestMethod, ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void XPathSelectorToRootTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/ClickTest.aspx");
                browser.Single("..", By.XPath);
            });
        }


        [TestMethod, ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void InvalidXPathSelectorToRootTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/ClickTest.aspx");
                browser.Single("///***-*///@@##@šš+š++++---><<>''", By.XPath);
            });
        }
    }
}