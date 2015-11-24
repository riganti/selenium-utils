using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;

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
            ExpectException(typeof(NoSuchElementException));
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

                browser.First("#button").CheckIfText(s=> s.ToLower().Contains("text"));
                browser.First("#input").CheckIfText(s => s.Contains("text"));
                browser.First("#area").CheckIfText(s => s.Contains("text"));


            });
        }
    }

   
}