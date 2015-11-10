using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;
using System.Threading;

namespace WebApplication1.Tests
{
    [TestClass]
    public class UiTests : SeleniumTestBase
    {
        [TestMethod]
        public void CheckIfIsDisplayed()
        {
            RunInAllBrowsers(browser =>
            {
                Log("log something");
                Thread.Sleep(5000);
                browser.NavigateToUrl();
                browser.CheckIfIsDisplayed("#displayed");
                browser.First("#displayed").CheckIfIsDisplayed();
            });
        }

        [TestMethod]
        public void CheckIfIsNotDisplayed()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsNotDisplayed("#non-displayed");
                browser.First("#non-displayed").CheckIfIsNotDisplayed();
            });
        }

        [TestMethod]
        public void GetFullSelector()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                TestContext.WriteLine(
                    browser.First("#displayed").FindElements("div p")
                    .FullSelector);
            });
        }

        [TestMethod]
        public void SearchInElementsCollection()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.FindElements("form").FindElements("div").ThrowIfSequenceEmpty();
            });
        }

        [TestMethod]
        public void SubSectionTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                RunTestSubSection(nameof(SubSectionAction), SubSectionAction);
            });
        }

        public void SubSectionAction(BrowserWrapper browser)
        {
            browser.FindElements("form").FindElements("div").ThrowIfDifferentCountThan(111);
        }

        [TestMethod]
        public void HasAttributeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
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

        [TestMethod]
        public void CheckCheckboxes()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Checkboxes.aspx");
                browser.First("#checkbox1").Wait(1200)
                                            .CheckIfIsChecked()
                                            .Wait(1200)
                                            .Click()
                                            .Wait(1200)
                                            .CheckIfIsNotChecked();

                browser.First("#checkbox2").CheckIfIsNotChecked()
                                            .Wait(1200)
                                            .Click()
                                            .Wait(1200)
                                            .CheckIfIsChecked();
            });
        }

        [TestMethod]
        public void NoParentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("NoParentTest.aspx");
                var parent = browser.First("html").ParentElement;
            });
        }

        [TestMethod]
        public void CollectionCheckClass()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("NoParentTest.aspx");
                var parent = browser.First("html").ParentElement;
            });
        }
    }
}