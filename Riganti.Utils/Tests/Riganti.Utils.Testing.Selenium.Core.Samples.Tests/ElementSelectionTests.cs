using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests
{
    [TestClass]
    public class ElementSelectionTests : SeleniumTest
    {
        [TestMethod]
        public void ElementSelection_ElementChildrenSelection()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementSelection");

                var elm = browser.First("#top-element");
                MSAssert.AreEqual(elm.Children.Count, 4);
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(EmptySequenceException))]
        public void ElementSelection_Single_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");

                browser.Single("nonexistent");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(NoSuchElementException))]
        public void ElementSelection_NoParentTest_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementSelection");
                var parent = browser.First("html").ParentElement;
            });
        }

        [TestMethod]
        public void ElementSelection_FindElements_SearchInElementsCollection()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                browser.First("#full-selector").FindElements("p").ThrowIfSequenceEmpty();
                browser.First("#full-selector").FindElements("div p").ThrowIfSequenceEmpty();
            });
        }

        [TestMethod]
        public void ElementSelection_FindElements_GetFullSelector()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                TestContext.WriteLine(
                    browser.First("#full-selector").FindElements("div p")
                        .FullSelector);

                TestContext.WriteLine(
                    browser.First("#full-selector-empty").FindElements("div p")
                        .FullSelector);
            });
        }

        [TestMethod]
        public void ElementSelection_ElementAtFirst()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/elementatfirst");
                MSAssert.AreEqual(
                    browser
                        .ElementAt("div > div", 0)
                        .First("#first0")
                        .GetInnerText()?.ToLower(), "first0");

                MSAssert.AreEqual(browser
                    .ElementAt("#divs > div", 1)
                    .First("div")
                    .GetInnerText()?.ToLower(), "first1");

                MSAssert.AreEqual(browser
                    .ElementAt("#divs > div", 2)
                    .ParentElement.First("#first2")
                    .GetInnerText()?.ToLower(), "first2");
            });
        }

        [TestMethod]
        public void ElementSelection_First_SelectBy()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/TemporarySelector");
                browser.SelectMethod = s => SelectBy.CssSelector(s, "[data-ui='{0}']");
                browser.First("p", By.TagName).CheckIfTextEquals("p");
                browser.First("id", By.Id).CheckIfTextEquals("id");
                browser.First("id").CheckIfTextEquals("data");
            });
        }
    }
}