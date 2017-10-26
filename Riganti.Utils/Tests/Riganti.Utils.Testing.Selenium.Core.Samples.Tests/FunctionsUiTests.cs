using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class FunctionsUiTests : SeleniumTest
    {
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

       [TestMethod]
        public void CookieTest()
        {
            Action<IBrowserWrapper> test = browser =>
            {
                browser.NavigateToUrl("/test/Cookies");
                browser.NavigateToUrl("/test/Cookies");
                browser.First("#CookieIndicator").CheckIfInnerTextEquals("default value");
                
                browser.Click("#SetCookies").Wait();
                browser.NavigateToUrl("/test/Cookies");
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

        [TestMethod]
        public void DragAndDropTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/DragAndDrop");
                browser.Wait(1000);
                var drag = browser.First("#draggable");
                var drop = browser.First("#droppable");
                browser.DragAndDrop(drag, drop);
                browser.WaitFor(() =>
                {
                    var result = browser.First("#droppable > p");
                    result.CheckIfTextEquals("Dropped!");
                }, 3000);
            });
        }

    }
}