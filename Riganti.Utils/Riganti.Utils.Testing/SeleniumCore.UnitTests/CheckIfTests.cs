using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using Selenium.Core.UnitTests.Mock;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class CheckIfTests
    {
        [TestMethod]
        public void CheckIfTagName_ArrayTest()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "frame" } } };
            var browser = new BrowserWrapper(driverMock, new MockISeleniumTest(), new ScopeOptions());

            var element = browser.First("frame");
            element.CheckIfTagName(new[] { "frame", "iframe" });

        }
        [TestMethod]
        public void CheckIfTagName_ArrayTest2()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "iframe" } } };
            var browser = new BrowserWrapper(driverMock, new MockISeleniumTest(), new ScopeOptions());

            var element = browser.First("iframe");
            element.CheckIfTagName(new[] { "frame", "iframe" });

        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfTagName_ArrayTest3()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "a" } } };
            var browser = new BrowserWrapper(driverMock, new MockISeleniumTest(), new ScopeOptions());

            var element = browser.First("a");
            element.CheckIfTagName(new[] { "frame", "iframe" });
        }

    }
}
