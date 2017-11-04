using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Api;
using Riganti.Selenium.Core.Exceptions;
using Selenium.Core.UnitTests.Mock;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class CheckIfTestsNewApi
    {
        [TestMethod]
        public void CheckIfTagName_ArrayTest()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "frame" } } };
            var browser = new BrowserWrapper(driverMock, new MockISeleniumTest(), new ScopeOptions());

            var element = browser.First("frame");
            AssertUI.CheckIfTagName(element, new[] { "frame", "iframe" });
        }


        [TestMethod]
        public void CheckIfTagName_ArrayTest2()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "iframe" } } };
            var browser = new BrowserWrapper(driverMock, new MockISeleniumTest(), new ScopeOptions());

            var element = browser.First("iframe");
            AssertUI.CheckIfTagName(element, new[] { "frame", "iframe" });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfTagName_ArrayTest3()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "a" } } };
            var browser = new BrowserWrapper(driverMock, new MockISeleniumTest(), new ScopeOptions());

            var element = browser.First("a");
            AssertUI.CheckIfTagName(element, new[] { "frame", "iframe" });
        }

    }
}