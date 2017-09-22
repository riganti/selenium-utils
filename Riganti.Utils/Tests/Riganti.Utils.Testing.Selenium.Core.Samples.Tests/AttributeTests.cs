using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class AttributeTests : SeleniumTest
    {
        [TestMethod]
        public void HasAttributeTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void CheckIfHasAttribute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#content").CheckIfHasAttribute("class");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException), AllowDerivedTypes = true)]
        public void CheckIfHasAttributeExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#content").CheckIfHasAttribute("title");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void HasAttributeTest3()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#submit-button").CheckIfHasAttribute("disabled");
            });
        }

        [TestMethod]
        public void HasAttributeTest4()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void CheckIfHasNotAttribute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                browser.First("#content").CheckIfHasNotAttribute("title");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfHasNotAttributeExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                browser.First("#content").CheckIfHasNotAttribute("class");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void HasAttributeTest2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void CheckAttributeTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#submit-button").CheckAttribute("type", "submit");
            });
        }
    }
}