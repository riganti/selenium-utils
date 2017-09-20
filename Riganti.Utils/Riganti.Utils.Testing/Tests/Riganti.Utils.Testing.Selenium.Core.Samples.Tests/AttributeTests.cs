using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class AttributeTests : SeleniumTest
    {
        [TestMethod]
        public void HasAttributeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void CheckIfHasAttribute()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#content").CheckIfHasAttribute("class");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException), AllowDerivedTypes = true)]
        public void CheckIfHasAttributeExpectedException()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#content").CheckIfHasAttribute("title");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void HasAttributeTest3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#submit-button").CheckIfHasAttribute("disabled");
            });
        }

        [TestMethod]
        public void HasAttributeTest4()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void CheckIfHasNotAttribute()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                browser.First("#content").CheckIfHasNotAttribute("title");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfHasNotAttributeExpectedException()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                browser.First("#content").CheckIfHasNotAttribute("class");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void HasAttributeTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void CheckAttributeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#submit-button").CheckAttribute("type", "submit");
            });
        }
    }
}