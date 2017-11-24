using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.UnitTests.Mock;
using Selenium.Core.UnitTests;

namespace Riganti.Selenium.Core.UnitTests
{
    [TestClass]
    public class CheckIfTests : MockingTest
    {
   
        [TestMethod]
        public void CheckIfTagName_ArrayTest()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "frame" } } };
            var browser = CreateMockedIBrowserWrapper(driverMock);

            var element = browser.First("frame");
            element.CheckIfTagName(new[] { "frame", "iframe" });

        }


        [TestMethod]
        public void CheckIfTagName_ArrayTest2()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "iframe" } } };
            var browser = CreateMockedIBrowserWrapper(driverMock);

            var element = browser.First("iframe");
            element.CheckIfTagName(new[] { "frame", "iframe" });

        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfTagName_ArrayTest3()
        {
            var driverMock = new MockIWebDriver { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "a" } } };
            var browser = CreateMockedIBrowserWrapper(driverMock);

            var element = browser.First("a");
            element.CheckIfTagName(new[] { "frame", "iframe" });
        }

    }
}
