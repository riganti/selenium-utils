using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.SeleniumCore;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.IO;
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
        public void CheckIfHasAttribute()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#content").CheckIfHasAttribute("class");
            });
        }

        [TestMethod]
        public void CheckIfHasAttributeExpectedException()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#content").CheckIfHasAttribute("title");
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
        public void CheckIfHasNotAttributeExpectedException()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                browser.First("#content").CheckIfHasNotAttribute("class");
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
            browser.FindElements("form").FindElements("div").ThrowIfDifferentCountThan(6);
        }

        [TestMethod]
        public void HasAttributeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.Wait(1000);
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
            });

            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.Wait(1000);
                browser.First("#dis-button").CheckIfHasNotAttribute("disabled");
            });

            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.Wait(1000);
                browser.First("#submit-button").CheckIfHasAttribute("disabled");
            });
        }

        [TestMethod]
        public void HasAttributeTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.Wait(1000);
                browser.First("#dis-button").CheckIfHasAttribute("disabled");
                browser.First("#submit-button").CheckIfHasNotAttribute("disabled");
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

        [TestMethod]
        public void UrlComparationTest1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/NoParentTest.aspx");
                browser.CheckUrl(url => url.Contains("NoParentTest.aspx"));
            });
        }

        [TestMethod]
        public void AlertTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("confirm test");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(SeleniumTestFailedException))]
        public void AlertTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("Confirm test", true);
            });
        }

        [TestMethod]
        public void AlertTest3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");

                browser.First("#button").Click();
                browser.CheckIfAlertTextContains("confirm");
            });
        }

        [TestMethod]
        public void AlertTest4()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test"), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void ExpectedExceptionTest()
        {
            ExpectException(typeof(WebDriverException), true);
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void ExpectedExceptionTest2()
        {
            ExpectException(typeof(AlertException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void ConfirmTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Confirm.aspx");

                var button = browser.First("#button");
                button.Click();
                browser.ConfirmAlert().First("#message").CheckIfInnerTextEquals("Accept", false);

                button.Click();
                browser.DismissAlert().First("#message").CheckIfInnerTextEquals("Dismiss", false);
            });
        }

        [TestMethod]
        public void SelectMethodTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/SelectMethod.aspx");

                Func<string, By> sMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = sMethod;

                var d = browser.First("d");
                d.SetCssSelectMethod();
                var c = d.First("#c");
                c.ParentElement.CheckIfHasAttribute("data-ui");

                //select css method - test switching
                browser.SetCssSelector();

                var a = browser.First("#a");
                a.SelectMethod = sMethod;
                var e = a.First("e");
                e.First("#b");

                a.SetBrowserSelectMethod();
                a.First("#c");
            });
        }

        [TestMethod]
        public void FileDialogTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("FileDialog.aspx");

                var tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, "test content");

                browser.FileUploadDialogSelect(browser.First("input[type=file]"), tempFile);
                browser.First("input[type=file]").CheckAttribute("value", string.IsNullOrWhiteSpace);

                File.Delete(tempFile);
            });
        }

        [TestMethod]
        public void TextTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("text.aspx");
                browser.First("#button").CheckIfTextEquals("text", false);
                browser.First("#input").CheckIfTextEquals("text", false);
                browser.First("#area").CheckIfTextEquals("text", false);

                browser.First("#button").CheckIfText(s => s.ToLower().Contains("text"));
                browser.First("#input").CheckIfText(s => s.Contains("text"));
                browser.First("#area").CheckIfText(s => s.Contains("text"));
            });
        }

        [TestMethod]
        public void JsInnerTextTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JsTestSite.aspx");
                var elm = browser.First("#hiddenElement");
                Assert.IsTrue(string.Equals(elm.GetJsInnerText()?.Trim(), "InnerText", StringComparison.OrdinalIgnoreCase));
                elm.CheckIfJsPropertyInnerText(c => c == "InnerText")
                    .CheckIfJsPropertyInnerTextEquals("InnerText", false);
            });
        }

        [TestMethod]
        public void JsInnerHtmlTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JsHtmlTest.aspx");
                var elm = browser.First("#htmlTest");
                Assert.IsTrue(string.Equals(elm.GetJsInnerHtml()?.Trim(), "<span></span>", StringComparison.OrdinalIgnoreCase));
                elm.CheckIfJsPropertyInnerHtmlEquals("<span></span>")
                    .CheckIfJsPropertyInnerHtml(c => c == "<span></span>");
            });
        }

        [TestMethod]
        public void ElementAtFirst1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("elementatfirst.aspx");
                Assert.AreEqual(
                                browser
                                .ElementAt("div > div", 0)
                                .First("#first0")
                                .GetInnerText()?.ToLower(), "first0");
            });
        }

        [TestMethod]
        public void ElementAtFirst2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("elementatfirst.aspx");
                Assert.AreEqual(browser
                                .ElementAt("#divs > div", 1)
                                .First("div")
                                .GetInnerText()?.ToLower(), "first1");
            });
        }

        [TestMethod]
        public void ElementAtFirst3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("elementatfirst.aspx");
                Assert.AreEqual(browser
                                .ElementAt("#divs > div", 2)
                                .ParentElement.First("#first2")
                                .GetInnerText()?.ToLower(), "first2");
            });
        }

        [TestMethod]
        public void First()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("TemporarySelector.aspx");
                browser.SelectMethod = s => SelectBy.CssSelector(s, "[data-ui='{0}']");
                browser.First("p", By.TagName).CheckIfTextEquals("p");
                browser.First("id", By.Id).CheckIfTextEquals("id");
                browser.First("id").CheckIfTextEquals("data");
            });
        }
    }
}