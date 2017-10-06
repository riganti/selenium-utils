using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class AttributeTests : SeleniumTest
    {
        [TestMethod]
        public void Attribute_CheckIfHasAttribute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");
                browser.First("#content").CheckIfHasAttribute("class");
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
            });
        }

        [TestMethod]
        public void Attribute_CheckIfHasNotAttribute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");

                browser.First("#content").CheckIfHasNotAttribute("title");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
            });
        }

        [TestMethod]
        public void Attribute_CheckIfHasAttribute_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#content").CheckIfHasAttribute("title");
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#submit-button").CheckIfHasAttribute("disabled");
                });
            });
        }

        [TestMethod]
        public void Attribute_CheckIfHasNotAttribute_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#content").CheckIfHasNotAttribute("class");
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#dis-button").CheckIfHasNotAttribute("disabled");
                });
            });
        }

        [TestMethod]
        public void Attribute_CheckAttribute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");
                browser.First("#submit-button").CheckAttribute("type", "submit");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void Attribute_CheckAttribute_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");
                browser.First("#submit-button").CheckAttribute("type", "button");
            });
        }
    }
}