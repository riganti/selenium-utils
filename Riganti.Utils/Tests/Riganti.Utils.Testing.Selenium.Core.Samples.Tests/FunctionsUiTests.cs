using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        #region DisplayedTests
        [TestMethod]
        public void Displayed_CheckIfIsDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Displayed");
                browser.CheckIfIsDisplayed("#displayed");
                browser.First("#displayed").CheckIfIsDisplayed();
                browser.First("#displayed-zero-draw-rec").CheckIfIsDisplayed();
            });
        }

        [TestMethod]
        public void Displayed_CheckIfIsNotDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Displayed");
                browser.CheckIfIsNotDisplayed("#non-displayed");
                browser.First("#non-displayed").CheckIfIsNotDisplayed();
            });
        }

        [TestMethod]
        public void Displayed_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Displayed");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#displayed").CheckIfIsNotDisplayed();
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#non-displayed").CheckIfIsDisplayed();
                });
            });
        }
        #endregion

        #region AttributeTests
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
        public void Attribute_CheckAttribute_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Attribute");
                browser.First("#submit-button").CheckAttribute("type", "button");
            });
        }
        #endregion

        #region ElementTests
        [TestMethod]
        [ExpectedSeleniumException(typeof(EmptySequenceException))]
        public void Element_Single_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");

                browser.Single("nonexistent");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(NoSuchElementException))]
        public void Element_NoParentTest_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/NoParentTest");
                var parent = browser.First("html").ParentElement;
            });
        }

        [TestMethod]
        public void Element_FindElements_SearchInElementsCollection()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                browser.First("#full-selector").FindElements("p").ThrowIfSequenceEmpty();
                browser.First("#full-selector").FindElements("div p").ThrowIfSequenceEmpty();
            });
        }

        [TestMethod]
        public void Element_FindElements_GetFullSelector()
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
        public void Element_ElementAtFirst()
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
        public void Element_First_SelectBy()
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
        #endregion

        #region AlertTests
        [TestMethod]
        public void Alert_CheckIfAlertText()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");

                browser.First("#button").Click();
                browser.CheckIfAlertTextEquals("confirm test");
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(AlertException))]
        public void Alert_CheckIfAlertText_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/alert");
                browser.First("#button").Click();
                browser.CheckIfAlertText(s => s.EndsWith("test."), "alert text doesn't end with 'test.'");
            });
        }

        [TestMethod]
        public void Alert_ConfirmAlert()
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
        #endregion

        #region SelectMethodTests
        [TestMethod]
        public void SelectMethod_Basic()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/SelectMethod");

                Func<string, By> selectMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = selectMethod;

                var outerElem = browser.First("outer-data-ui");
                var innerElem = outerElem.First("inner-data-ui");

                innerElem.CheckIfHasAttribute("data-ui");
                innerElem.CheckIfHasNotAttribute("id");
                innerElem.ParentElement.CheckIfHasNotAttribute("data-ui");
            });
        }

        [TestMethod]
        public void SelectMethod_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/SelectMethod");

                Func<string, By> selectMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = selectMethod;

                var outerElem = browser.First("outer-data-ui");
                MSAssert.ThrowsException<NoSuchElementException>(() =>
                {
                    var innerElem = outerElem.First("#inner-id");
                });
            });
        }

        [TestMethod]
        public void SelectMethod_SwitchElementSelector()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/SelectMethod");

                Func<string, By> selectMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = selectMethod;

                var outerElem = browser.First("outer-data-ui");
                outerElem.SetCssSelectMethod();
                var innerElem = outerElem.First("#inner-id");

                innerElem.CheckIfHasAttribute("id");
                innerElem.CheckIfHasNotAttribute("data-ui");
                innerElem.ParentElement.CheckIfHasNotAttribute("id");
            });
        }

        [TestMethod]
        public void SelectMethod_SwitchBrowserSelector()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/SelectMethod");

                Func<string, By> selectMethod = s => By.CssSelector($"[data-ui='{s}']");
                browser.SelectMethod = selectMethod;

                var outerElem = browser.First("outer-data-ui");
                outerElem.SetCssSelectMethod();

                browser.SetCssSelector();
                var innerElem = browser.First("#inner-id");

                innerElem.CheckIfHasAttribute("id");
                innerElem.CheckIfHasNotAttribute("data-ui");
            });
        }
        #endregion

        #region TextTests
        [TestMethod]
        public void Text_CheckIfTextEquals()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
                browser.First("#button").CheckIfTextEquals("text", false);
                browser.First("#input").CheckIfTextEquals("text", false);

                browser.First("#area").CheckIfTextEquals("TeXt", false);
                browser.First("#area").CheckIfTextEquals("text");
            });
        }

        [TestMethod]
        public void Text_CheckIfTextEquals_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#input").CheckIfTextEquals("text2", false);
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#area").CheckIfTextEquals("TeXt");
                });
            });
        }

        [TestMethod]
        public void Text_CheckIfText()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
                browser.First("#button").CheckIfText(s => s.ToLower().Contains("text"));
                browser.First("#input").CheckIfText(s => s.Contains("text"));
                browser.First("#area").CheckIfText(s => s.Contains("text"));
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void Text_CheckIfText_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
                browser.First("#input").CheckIfText(s => !s.Contains("text"));
            });
        }
        #endregion

        #region JsTests
        [TestMethod]
        public void Js_SetJsElementProperty()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JSView");
                var input = browser.First("#input-set");
                const string inputValue = "new value";
                input.SetJsElementProperty("value", inputValue);
                input.CheckIfValue(inputValue);
            });
        }

        [TestMethod]
        public void Js_GetJsElementPropertyValue()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JSView");
                var input = browser.First("#input-get");
                input.CheckIfValue("input1");
                MSAssert.AreEqual(input.GetJsElementPropertyValue("value"), "input1");
            });
        }

        [TestMethod]
        public void Js_CheckIfJsPropertyInnerText()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JSView");
                var elm = browser.First("#input-hidden");
                MSAssert.IsTrue(string.Equals(elm.GetJsInnerText()?.Trim(), "InnerText",
                    StringComparison.OrdinalIgnoreCase));
                elm.CheckIfJsPropertyInnerText(c => c == "InnerText")
                    .CheckIfJsPropertyInnerTextEquals("InnerText", false);
            });
        }

        [TestMethod]
        public void Js_CheckIfJsPropertyInnerHtml()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/JSView");
                var elm = browser.First("#htmlTest");
                var content = elm.GetJsInnerHtml()?.Trim() ?? "";
                MSAssert.IsTrue(content.Contains("<span>") && content.Contains("</span>"));
                elm.CheckIfJsPropertyInnerHtml(c => c.Contains("<span>") && c.Contains("</span>"));
            });
        }
        #endregion

        #region ElementContained
        [TestMethod]
        [ExpectedSeleniumException(typeof(EmptySequenceException))]
        public void ElementContained_CheckIfContainsElement_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");

                var elem = browser.First("#none");
                elem.CheckIfContainsElement("span");
            });
        }

        [TestMethod]
        public void ElementContained_CheckIfNotContainsElement_NoElement()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                browser.First("#none").CheckIfNotContainsElement("span");
            });
        }

        [TestMethod]
        public void ElementContained_CheckIfNotContainsElement_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                MSAssert.ThrowsException<MoreElementsInSequenceException>(() =>
                {
                    browser.First("#one").CheckIfNotContainsElement("span");
                });
                MSAssert.ThrowsException<MoreElementsInSequenceException>(() =>
                {
                    browser.First("#two").CheckIfNotContainsElement("span");
                });
            });
        }

        [TestMethod]
        public void ElementContained_CheckIfContainsElement()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementContained");
                browser.First("#one").CheckIfContainsElement("span");
                browser.First("#two").CheckIfContainsElement("span");
            });
        }
        #endregion

        #region ValueTests
        [TestMethod]
        public void Value_CheckIfValue()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/value");
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
        public void Value_CheckIfValue_ExpectedException()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/value");
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#input-radio2").CheckIfValue("radio1");
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#area").CheckIfValue("wrongvalue");
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#input-text").CheckIfValue("texT1");
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#input-text").CheckIfValue("   text1   ", trimValue: false);
                });
            });
        }
        #endregion

        #region UrlTests
        [TestMethod]
        public void Url_CheckHyperLink_Relative()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");
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
            });
        }

        [TestMethod]
        public void Url_CheckHyperLink_Absolute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");
                browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.AbsoluteUri);
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema", "//localhost:1234/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
                browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema",
                    "//localhostads:1234/path/test?query=test#fragment",
                    UrlKind.Absolute, UriComponents.PathAndQuery);
            });
        }

        [TestMethod]
        public void Url_CheckHyperLink_Relative_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#RelativeLink", "/path0/test?query=test#fragment", UrlKind.Relative,
                        UriComponents.PathAndQuery);
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#RelativeLink", "/path/test?query=test#fragment_nonexistent",
                        UrlKind.Relative, UriComponents.AbsoluteUri);
                });
            });
        }

        [TestMethod]
        public void Url_CheckHyperLink_Absolute_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path0/test?query=test#fragment",
                        UrlKind.Absolute, UriComponents.PathAndQuery);
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteSameSchema",
                        "https://localhost:1234/path/test?query=test#fragment", UrlKind.Absolute,
                        UriComponents.AbsoluteUri);
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.CheckIfHyperLinkEquals("#AbsoluteLink", "https://www.google.com/path/test?query=test#fragment_nonexistent",
                        UrlKind.Absolute, UriComponents.AbsoluteUri);
                });
            });
        }

        [TestMethod]
        public void Url_CheckUrl()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");
                browser.CheckUrl(url => url.Contains("/test/HyperLink"));
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(BrowserLocationException))]
        public void Url_CheckUrl_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");
                browser.CheckUrl(url => url.Contains("/test/HyperLink_nonexistent"));
            });
        }

        [TestMethod]
        public void Url_CheckIfUrlExists()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.CheckIfUrlIsAccessible("/test/HyperLink", UrlKind.Relative);
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(WebException))]
        public void Url_CheckIfUrlExists_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.CheckIfUrlIsAccessible("/test/HyperLink_notexisting", UrlKind.Relative);
            });
        }
        #endregion

        [TestMethod]
        public void CookieTest()
        {
            Action<IBrowserWrapper> test = browser =>
            {
                browser.NavigateToUrl("/test/CookiesTest");
                browser.NavigateToUrl("/test/CookiesTest");
                browser.First("#CookieIndicator").CheckIfInnerTextEquals("default value");
                
                browser.Click("#SetCookies").Wait();
                browser.NavigateToUrl("/test/CookiesTest");
                browser.First("#CookieIndicator").CheckIfInnerTextEquals("new value");
            };
            this.RunInAllBrowsers(test);
            this.RunInAllBrowsers(test);
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
    }
}