using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using System;
using System.IO;
using System.Threading;
using Riganti.Utils.Testing.Selenium.Core.Api;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class FunctionsUiTestsNewApi : SeleniumTest
    {
        [TestMethod]
        public void CheckIfIsDisplayed()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsDisplayed("#displayed");
                AssertUI.CheckIfIsDisplayed(browser.First("#displayed"));
            });
        }

        [TestMethod]
        public void CheckIfIsNotDisplayed()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsNotDisplayed("#non-displayed");
                AssertUI.CheckIfIsNotDisplayed(browser.First("#non-displayed"));
                AssertUI.CheckIfIsDisplayed(browser.First("#displayed-zero-draw-rec"));
            });
        }

        [TestMethod]
        public void CheckIfHasAttribute()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfHasAttribute(browser.First("#content"), "class");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException), AllowDerivedTypes = true)]
        public void CheckIfHasAttributeExpectedException()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfHasAttribute(browser.First("#content"), "title");
            });
        }

        [TestMethod]
        public void CheckIfHasNotAttribute()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                AssertUI.CheckIfHasNotAttribute(browser.First("#content"), "title");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfHasNotAttributeExpectedException()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

                AssertUI.CheckIfHasNotAttribute(browser.First("#content"), "class");
            });
        }

        [TestMethod]
        public void GetFullSelector()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Context.WriteLine(
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
#pragma warning disable CS0612 // Type or member is obsolete
                RunTestSubSection("Test Subsection", (b) => { });
#pragma warning restore CS0612 // Type or member is obsolete
            });
        }

        [TestMethod]
        public void HasAttributeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfHasAttribute(browser.First("#dis-button"), "disabled");
                AssertUI.CheckIfHasNotAttribute(browser.First("#submit-button"), "disabled");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void HasAttributeTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfHasNotAttribute(browser.First("#dis-button"), "disabled");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void HasAttributeTest3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfHasAttribute(browser.First("#submit-button"), "disabled");
            });
        }

        [TestMethod]
        public void HasAttributeTest4()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfHasAttribute(browser.First("#dis-button"), "disabled");
                AssertUI.CheckIfHasNotAttribute(browser.First("#submit-button"), "disabled");
            });
        }

        [TestMethod]
        public void CheckAttributeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckAttribute(browser.First("#submit-button"), "type", "submit");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuchElementException))]
        public void NoParentTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("NoParentTest.aspx");
                var parent = browser.First("html").ParentElement;
            });
        }

        [TestMethod]
        public void UrlComparisonTest1()
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
        public void AlertTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");

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
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test"), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AlertException))]
        public void ExpectedExceptionTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Alert.aspx");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(AlertException))]
        public void ExpectedExceptionTest2()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Confirm.aspx");

                var button = browser.First("#button");
                button.Click();

                AssertUI.CheckIfInnerText(browser.ConfirmAlert().First("#message"), s => s.Equals("Accept", StringComparison.OrdinalIgnoreCase));

                button.Click();
                AssertUI.CheckIfInnerText(browser.DismissAlert().First("#message"), s => s.Equals("Dismiss", StringComparison.OrdinalIgnoreCase));
            });
        }

        [TestMethod]
        public void SelectMethodTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/SelectMethod.aspx");

                Func<string, By> sMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = sMethod;

                var d = browser.First("d");
                d.SetCssSelectMethod();
                var c = d.First("#c");

                AssertUI.CheckIfHasAttribute(c.ParentElement, "data-ui");

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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("FileDialog.aspx");

                var tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, "test content");

                browser.FileUploadDialogSelect(browser.First("input[type=file]"), tempFile);
                AssertUI.CheckAttribute(browser.First("input[type=file]"), "value", s => !string.IsNullOrWhiteSpace(s));

                File.Delete(tempFile);
            });
        }

        [TestMethod]
        public void TextTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("text.aspx");
                AssertUI.CheckIfTextEquals(browser.First("#button"), "text", false);
                AssertUI.CheckIfTextEquals(browser.First("#input"), "text", false);
                AssertUI.CheckIfTextEquals(browser.First("#area"), "text", false);

                AssertUI.CheckIfText(browser.First("#button"), s => s.ToLower().Contains("text"));
                AssertUI.CheckIfText(browser.First("#input"), s => s.Contains("text"));
                AssertUI.CheckIfText(browser.First("#area"), s => s.Contains("text"));
            });
        }

        [TestMethod]
        public void JsInnerTextTest()
        {
            RunInAllBrowsers(browser =>
            {
                SeleniumTestsConfiguration.DeveloperMode = true;
                browser.NavigateToUrl("JsTestSite.aspx");
                var elm = browser.First("#hiddenElement");
                Assert.IsTrue(string.Equals(elm.GetJsInnerText()?.Trim(), "InnerText",
                    StringComparison.OrdinalIgnoreCase));
                AssertUI.CheckIfJsPropertyInnerText(elm, c => c == "InnerText");
                AssertUI.CheckIfJsPropertyInnerTextEquals(elm, "InnerText", false);
            });
        }

        [TestMethod]
        public void JsInnerHtmlTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JsHtmlTest.aspx");
                var elm = browser.First("#htmlTest");
                var content = elm.GetJsInnerHtml()?.Trim() ?? "";
                Assert.IsTrue(content.Contains("<span>") && content.Contains("</span>"));
                AssertUI.CheckIfJsPropertyInnerHtml(elm, c => c.Contains("<span>") && c.Contains("</span>"));
            });
        }

        [TestMethod]
        public void ElementAtFirst1()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("TemporarySelector.aspx");
                browser.SelectMethod = s => SelectBy.CssSelector(s, "[data-ui='{0}']");
                AssertUI.CheckIfTextEquals(browser.First("p", By.TagName), "p");
                AssertUI.CheckIfTextEquals(browser.First("id", By.Id), "id");
                AssertUI.CheckIfTextEquals(browser.First("id"), "data");
            });
        }

        [TestMethod]
        [ExpectedException(typeof(EmptySequenceException))]
        public void ElementContained_NoElement_ExpectedFailure()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");

                var a = browser.First("#no");
                AssertUI.CheckIfContainsElement(a, "span");
            });
        }

        [TestMethod]
        public void ElementContained_NoElement()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");
                AssertUI.CheckIfNotContainsElement(browser.First("#no"), "span");
            });
        }

        [TestMethod]
        public void ElementContained_OneElement_ExpectedFailure()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            try
            {
                RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("ElementContained.aspx");
                    AssertUI.CheckIfNotContainsElement(browser.First("#one"), "span");
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("children"))
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ElementContained_OneElement()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");
                AssertUI.CheckIfContainsElement(browser.First("#one"), "span");
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
                RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl("ElementContained.aspx");
                    AssertUI.CheckIfNotContainsElement(browser.First("#two"), "span");
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ElementContained.aspx");
                AssertUI.CheckIfContainsElement(browser.First("#two"), "span");
            });
        }

        [TestMethod]
        public void CheckValueTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("valuetest.aspx");
                AssertUI.CheckIfValue(browser.First("#input-radio"), "radio1");
                AssertUI.CheckIfValue(browser.First("#input-radio2"), "radio2");
                AssertUI.CheckIfValue(browser.First("#checkbox1"), "checkboxvalue1");
                AssertUI.CheckIfValue(browser.First("#checkbox2"), "checkboxvalue2");
                AssertUI.CheckIfValue(browser.First("#area"), "areavalue");
                AssertUI.CheckIfValue(browser.First("#input-text"), "text1");
                AssertUI.CheckIfValue(browser.First("#input-text"), "texT1", false);
                AssertUI.CheckIfValue(browser.First("#input-text"), "   texT1   ", false);
            });
        }

        [TestMethod]
        public void SetJsInputPropertyTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("JSPropertySetTest.aspx");
                var input = browser.First("#input1");
                const string inputValue = "4012 5770 5655";
                input.SetJsElementProperty("value", inputValue);
                AssertUI.CheckIfValue(input, inputValue);
                Assert.AreEqual(input.GetJsElementPropertyValue("value"), inputValue);
            });
        }

        [TestMethod]
        public void CookieTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            Action<BrowserWrapper> test = browser =>
            {
                browser.NavigateToUrl("CookiesTest.aspx");
                AssertUI.CheckIfTextEquals(browser.First("#CookieIndicator"), "False");
                browser.Click("#SetCookies").Wait();
                browser.NavigateToUrl("CookiesTest.aspx");
                AssertUI.CheckIfTextEquals(browser.First("#CookieIndicator"), "True");
            };
            RunInAllBrowsers(test);
            RunInAllBrowsers(test);
        }

        [TestMethod]
        public void TextNotEquals()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("CookiesTest.aspx");
                var label = browser.First("#CookieIndicator");
                AssertUI.CheckIfTextEquals(label, "False");
                AssertUI.CheckIfTextNotEquals(label, "True");
                try
                {
                    AssertUI.CheckIfTextNotEquals(label, "False");
                    throw new Exception("Exception was expected.");
                }
                catch (UnexpectedElementStateException)
                {
                }
            });
        }

        [TestMethod]
        public void CheckHyperLink()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                browser.CheckIfHyperLinkEqualsApi("#AbsoluteSameSchema", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEqualsApi("#RelativeLink", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEqualsApi("#RelativeLink", "/path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEqualsApi("#RelativeLink", "path/test?query=test#fragmentasd", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEqualsApi("#RelativeLink", "path/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEqualsApi("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEqualsApi("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEqualsApi("#AbsoluteSameSchema", "//localhost:1234/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEqualsApi("#AbsoluteSameSchema",
                    "//localhostads:1234/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.PathAndQuery);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckHyperLink_Failure1()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                browser.CheckIfHyperLinkEqualsApi("#RelativeLink", "/path0/test?query=test#fragment", UrlKind.Relative,
                    UriComponents.PathAndQuery);
            });
        }


        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckHyperLink_Failure2()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                browser.CheckIfHyperLinkEquals("#RelativeLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute);
                browser.CheckIfHyperLinkEqualsApi("#RelativeLink", "/path0/test?query=test#fragment", UrlKind.Relative,
    UriComponents.PathAndQuery);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckHyperLink_Failure3()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                browser.CheckIfHyperLinkEqualsApi("#AbsoluteLink", "https://www.googles.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.AbsoluteUri);
            });
        }

        [TestMethod]

        public void CheckHyperLink_Failure4()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                try
                {
                    browser.CheckIfHyperLinkEqualsApi("#AbsoluteSameSchema", "https://localhost:1234/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.AbsoluteUri);
                }
                catch (UnexpectedElementStateException)
                {
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(EmptySequenceException))]
        public void SingleExceptionTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("hyperlink.aspx");
                browser.Single("asdasd");
            });
        }

        [TestMethod]
        public void CheckIfUrlExistsTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.CheckIfUrlIsAccessible("hyperlink.aspx", UrlKind.Relative);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(SeleniumTestFailedException))]
        public void CheckIfUrlExistsTest2()
        {
            SeleniumTestsConfiguration.DeveloperMode = false;
            SeleniumTestsConfiguration.PlainMode = false;
            RunInAllBrowsers(browser =>
            {
                browser.CheckIfUrlIsAccessible("NonExistent359.aspx", UrlKind.Relative);
            });
        }
    }
}