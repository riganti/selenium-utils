using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class FunctionsUiTests : SeleniumTest
    {
        [TestMethod]
        public void CheckIfIsDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsDisplayed("#dispblayed");
                browser.First("#displayed").CheckIfIsDisplayed();
            });
        }

        [TestMethod]
        public void CheckIfIsNotDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsNotDisplayed("#non-displayed");
                browser.First("#non-displayed").CheckIfIsNotDisplayed();
                browser.First("#displayed-zero-draw-rec").CheckIfIsDisplayed();
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
        public void GetFullSelector()
        {
            this.RunInAllBrowsers(browser =>
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
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.FindElements("form").FindElements("div").ThrowIfSequenceEmpty();
            });
        }



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
        public void CheckAttributeTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.First("#submit-button").CheckAttribute("type", "submit");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuchElementException))]
        public void NoParentTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/NoParentTest");
                var parent = browser.First("html").ParentElement;
            });
        }

        [TestMethod]
        public void UrlComparisonTest1()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/NoParentTest");
                browser.CheckUrl(url => url.Contains("/test/NoParentTest"));
            });
        }

        [TestMethod]
        public void AlertTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("confirm test");
            });
        }

        [TestMethod]
        public void AlertTest2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                try
                {
                    browser.CheckIfAlertTextEquals("Confirm test", true);
                }
                catch (AlertException)
                {
                }
            });
        }


        [TestMethod]
        public void AlertTestN()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("confirm test");
            });
        }

        [TestMethod]
        public void AlertTest2N()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                try
                {
                    browser.CheckIfAlertTextEquals("Confirm test", true);
                }
                catch (AlertException)
                {
                }
            });
        }

        [TestMethod]
        public void AlertTest3()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextContains("confirm");
            });
        }

        [TestMethod]
        public void AlertTest4()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test"), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AlertException))]
        public void ExpectedExceptionTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AlertException))]
        public void ExpectedExceptionTest2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void ConfirmTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Confirm");

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
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/SelectMethod");

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
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FileDialog");

                var tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, "test content");

                browser.FileUploadDialogSelect(browser.First("input[type=file]"), tempFile);
                browser.First("input[type=file]").CheckAttribute("value", s => !string.IsNullOrWhiteSpace(s));

                File.Delete(tempFile);
            });
        }

        [TestMethod]
        public void TextTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
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
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JsTestSite");
                var elm = browser.First("#hiddenElement");
                MSAssert.IsTrue(string.Equals(elm.GetJsInnerText()?.Trim(), "InnerText",
                    StringComparison.OrdinalIgnoreCase));
                elm.CheckIfJsPropertyInnerText(c => c == "InnerText")
                    .CheckIfJsPropertyInnerTextEquals("InnerText", false);
            });
        }

        [TestMethod]
        public void JsInnerHtmlTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JsHtmlTest");
                var elm = browser.First("#htmlTest");
                var content = elm.GetJsInnerHtml()?.Trim() ?? "";
                MSAssert.IsTrue(content.Contains("<span>") && content.Contains("</span>"));
                elm.CheckIfJsPropertyInnerHtml(c => c.Contains("<span>") && c.Contains("</span>"));
            });
        }

        [TestMethod]
        public void ElementAtFirst1()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/elementatfirst");
                MSAssert.AreEqual(
                    browser
                        .ElementAt("div > div", 0)
                        .First("#first0")
                        .GetInnerText()?.ToLower(), "first0");
            });
        }

        [TestMethod]
        public void ElementAtFirst2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/elementatfirst");
                MSAssert.AreEqual(browser
                    .ElementAt("#divs > div", 1)
                    .First("div")
                    .GetInnerText()?.ToLower(), "first1");
            });
        }

        [TestMethod]
        public void ElementAtFirst3()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/elementatfirst");
                MSAssert.AreEqual(browser
                    .ElementAt("#divs > div", 2)
                    .ParentElement.First("#first2")
                    .GetInnerText()?.ToLower(), "first2");
            });
        }

        [TestMethod]
        public void First()
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

        [TestMethod]
        [ExpectedSeleniumException(typeof(EmptySequenceException))]
        public void ElementContained_NoElement_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");

                var a = browser.First("#no");
                a.CheckIfContainsElement("span");
            });
        }

        [TestMethod]
        public void ElementContained_NoElement()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                browser.First("#no").CheckIfNotContainsElement("span");
            });
        }

        [TestMethod]


        public void ElementContained_OneElement_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                MSAssert.ThrowsException<MoreElementsInSequenceException>(() =>{
                    browser.First("#one").CheckIfNotContainsElement("span");
                });
            });
        }

        [TestMethod]
        public void ElementContained_OneElement()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                browser.First("#one").CheckIfContainsElement("span");
            });

            try
            {
            }
            catch (Exception ex)
            {
                var message = ex.ToString();

                if (message.Contains("child") || !message.Contains("2"))
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ElementContained_TwoElement_ExpectedFailure()
        {
            try
            {
                this.RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("/test/ElementContained");
                    browser.First("#two").CheckIfNotContainsElement("span");
                });
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                if (!message.Contains("children") || !message.Contains("2"))
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ElementContained_TwoElement()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                browser.First("#two").CheckIfContainsElement("span");
            });
        }

        [TestMethod]
        public void CheckValueTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/valuetest");
                browser.First("#input-radio").CheckIfValue("radio1");
                browser.First("#input-radio2").CheckIfValue("radio2");
                browser.First("#checkbox1").CheckIfValue("checkboxvalue1");
                browser.First("#checkbox2").CheckIfValue("checkboxvalue2");
                browser.First("#area").CheckIfValue("areavalue");
                browser.First("#input-text").CheckIfValue("text1");
                browser.First("#input-text").CheckIfValue("texT1", true);
                browser.First("#input-text").CheckIfValue("   texT1   ", true);
            });
        }

        [TestMethod]
        public void SetJsInputPropertyTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JSPropertySetTest");
                var input = browser.First("#input1");
                const string inputValue = "4012 5770 5655";
                input.SetJsElementProperty("value", inputValue);
                input.CheckIfValue(inputValue);
                MSAssert.AreEqual(input.GetJsElementPropertyValue("value"), inputValue);
            });
        }

        [TestMethod]
        public void CookieTest()
        {
            Action<IBrowserWrapper> test = browser =>
            {
                browser.NavigateToUrl("/test/CookiesTest");
                browser.First("#CookieIndicator").CheckIfTextEquals("False");
                browser.Click("#SetCookies").Wait();
                browser.NavigateToUrl("/test/CookiesTest");
                browser.First("#CookieIndicator").CheckIfTextEquals("True");
            };
            this.RunInAllBrowsers(test);
            this.RunInAllBrowsers(test);
        }

        [TestMethod]
        public void TextNotEquals_ExceptionExpected()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/CookiesTest");
                var label = browser.First("#CookieIndicator");
                label.CheckIfTextNotEquals("True");
                label.CheckIfTextEquals("False");
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    label.CheckIfTextNotEquals("False");
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/hyperlink");
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema", "/path/test?query=test#fragment",
                    UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "path/test?query=test#fragmentasd", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#RelativeLink", "path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema", "//localhost:1234/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema",
                    "//localhostads:1234/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.PathAndQuery);
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure1()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/hyperlink");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#RelativeLink", "/path0/test?query=test#fragment", UrlKind.Relative,
                        UriComponents.PathAndQuery);
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/hyperlink");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#RelativeLink", "https://www.google.com/path/test?query=test#fragment",
                        UrlKind.Absolute);
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure3()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/hyperlink");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.googles.com/path/test?query=test#fragment",
                        UrlKind.Absolute, UriComponents.AbsoluteUri);
                });
            });
        }

        [TestMethod]
        public void CheckHyperLink_Failure4()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/hyperlink");
                try
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema",
                        "https://localhost:1234/path/test?query=test#fragment", UrlKind.Absolute,
                        UriComponents.AbsoluteUri);
                }
                catch (UnexpectedElementStateException)
                {
                }
            });
        }

        [TestMethod]
        public void SingleExceptionTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/hyperlink");

                MSAssert.ThrowsException<EmptySequenceException>(() =>
                {
                    browser.Single("asdasd");
                });
            });
        }

        [TestMethod]
        public void CheckIfUrlExistsTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                MSAssert.ThrowsException<EmptySequenceException>(() =>
                {
                    browser.CheckIfUrlIsAccessible("/test/hyperlink", UrlKind.Relative);
                });
            });
        }

        [TestMethod]
        public void CheckIfUrlExistsTest2()
        {
            this.RunInAllBrowsers(browser =>
            {
                MSAssert.ThrowsException<SeleniumTestFailedException>(() =>
                {
                    browser.CheckIfUrlIsAccessible("/test/NonExistent359", UrlKind.Relative);
                });
            });
        }
    }
}