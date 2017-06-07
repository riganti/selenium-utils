using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class BrowsersStabilityUiTests : SeleniumTest
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
        [TestMethod, ExpectedException(typeof(Exception))]
        public void XPathSelectorToRootTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/ClickTest.aspx");
                browser.Single("..", By.XPath);
            });
        }


        [TestMethod, ExpectedException(typeof(Exception))]
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
