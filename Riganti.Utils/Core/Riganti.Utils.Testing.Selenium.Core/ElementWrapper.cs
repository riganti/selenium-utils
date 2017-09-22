using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Comparators;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class ElementWrapper : ISeleniumWrapper, IElementWrapper
    {
        private readonly IWebElement element;
        private readonly IBrowserWrapper browser;

        /// <summary>
        /// Gets selector used to get this element.
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// Generated css selector to this element.
        /// </summary>
        public string FullSelector => GenerateFullSelector();

        /// <summary>
        /// Activates selenium SwitchTo function to change context to make element accessable.
        /// </summary>
        public void ActivateScope()
        {
            ParentWrapper.ActivateScope();
        }

        /// <summary>
        /// Default timeout for Wait function.
        /// </summary>
        public int ActionWaitTime { get; set; }

        /// <summary>
        /// Unwrapped IWebElement binding implementation (SeleniumHQ).
        /// </summary>
        public IWebElement WebElement
        {
            get
            {
                ParentWrapper.ActivateScope();
                return element;
            }
        }

        public IBrowserWrapper BrowserWrapper => browser;

        /// <summary>
        /// Parent wrapper
        /// </summary>
        public ISeleniumWrapper ParentWrapper { get; set; }

        private Func<string, By> selectMethod = null;

        public Func<string, By> SelectMethod
        {
            get { return selectMethod ?? browser.SelectMethod; }
            set { selectMethod = value; }
        }

        /// <summary>
        /// Returns html direct parent element.
        /// </summary>
        public IElementWrapper ParentElement
        {
            get
            {
                IElementWrapper parent;
                try
                {
                    parent = First("..", By.XPath);
                }
                catch (Exception ex)
                {
                    throw new NoSuchElementException($"Parent element of '{FullSelector}' was not found!", ex);
                }
                if (parent == null)
                    throw new NoSuchElementException($"Parent element of '{FullSelector}' was not found!");
                return parent;
            }
        }

        /// <summary>
        /// Contains direct children of the element.
        /// </summary>
        public IElementWrapperCollection Children => FindElements("child::*", By.XPath);

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementWrapper"/> class.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="browserWrapper">The browser wrapper.</param>
        /// <param name="selector">The selector.</param>
        public ElementWrapper(IWebElement webElement, IBrowserWrapper browserWrapper, string selector = null)
        {
            element = webElement;
            browser = browserWrapper;
            Selector = selector;
            SelectMethod = browser.SelectMethod;
            BaseUrl = browser.BaseUrl;
            ActionWaitTime = browserWrapper.ActionWaitTime;
        }

        /// <summary>
        /// Absolute Url that is used to resolve relative addresses when function NavigateTo is used.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Set css selector as current select method.
        /// </summary>
        public void SetCssSelectMethod()
        {
            selectMethod = SelectBy.CssSelector;
        }

        /// <summary>
        /// Resets select method to value of BrowserWrapper.
        /// </summary>
        public void SetBrowserSelectMethod()
        {
            selectMethod = null;
        }

        /// <summary>
        /// Sends enter key to browser.
        /// </summary>
        /// <returns></returns>
        public IElementWrapper SendEnterKey()
        {
            SendKeys(Keys.Enter);
            return this;
        }

        private string GenerateFullSelector()
        {
            var parent = ParentWrapper as ElementWrapperCollection;
            if (parent != null)
            {
                var index = parent.IndexOf(this);
                var parentSelector = string.IsNullOrWhiteSpace(parent.FullSelector) ? "" : parent.FullSelector.Trim() + $":nth-child({index + 1})";
                return $"{parentSelector}".Trim();
            }
            return $"{Selector ?? ""}".Trim();
        }

        /// <summary>
        /// Checks the name of the tag.
        /// </summary>
        /// <param name="expectedTagName">Expected name of the tag.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns></returns>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public virtual IElementWrapper CheckTagName(string expectedTagName, string failureMessage = null)
        {
            if (!string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException(failureMessage ?? $"Element has wrong tagName. Expected value: '{expectedTagName}', Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// Checks the name of tag.
        /// </summary>
        /// <param name="expectedTagNames">The expected tag names.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns></returns>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public virtual IElementWrapper CheckIfTagName(string[] expectedTagNames, string failureMessage = null)
        {
            var valid = false;

            foreach (var expectedTagName in expectedTagNames)
            {
                if (string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
                {
                    valid = true;
                }
            }

            if (!valid)
            {
                var allowed = string.Join(", ", expectedTagNames);
                throw new UnexpectedElementStateException(failureMessage ?? $"Element has wrong tagName. Expected value: '{allowed}', Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// Checks the name of tag.
        /// </summary>
        /// <param name="expectedTagName">Expected name of the tag.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns></returns>
        public virtual IElementWrapper CheckIfTagName(string expectedTagName, string failureMessage = null)
        {
            return CheckTagName(expectedTagName, failureMessage);
        }

        /// <summary>
        /// Checks if this element contains other element(s) selected by <see cref="cssSelector"/>.
        /// </summary>
        /// <param name="cssSelector">The CSS selector.</param>
        /// <param name="tmpSelectMethod">The temporary select method.</param>
        /// <returns></returns>
        /// <exception cref="EmptySequenceException"></exception>
        public virtual IElementWrapper CheckIfContainsElement(string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            if (FindElements(cssSelector, tmpSelectMethod).Count == 0)
            {
                throw new EmptySequenceException($"This element ('{FullSelector}') does not contain child selectable by '{cssSelector}'.");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfNotContainsElement(string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            var count = FindElements(cssSelector, tmpSelectMethod).Count;
            if (count != 0)
            {
                var children = count == 1 ? "child" : $"children ({count})";
                throw new MoreElementsInSequenceException($"This element ('{FullSelector}') contains {children} selectable by '{cssSelector}' and should not.");
            }
            return this;
        }

        public virtual string GetJsElementPropertyValue(string elementPropertyName)
        {
            var obj = browser.GetJavaScriptExecutor()?.ExecuteScript(@"return (arguments || [{}])[0]['" + elementPropertyName + "'];", WebElement);
            return obj?.ToString();
        }

        /// <summary>
        /// Inserts javascript to the site and returns value of innerText/textContent property of this element.
        /// </summary>
        public virtual IElementWrapper SetJsElementProperty(string propertyName, object propertyValue)
        {
            browser.GetJavaScriptExecutor()?.ExecuteScript(" var a = (arguments || [{}, null, null]);    var element = a[0];    var propertyName = a[1];    var propertyValue = a[2]; element[propertyName] = propertyValue;", WebElement, propertyName, propertyValue);
            return this;
        }

        /// <summary>
        /// Inserts javascript to the site and returns value of innerText/textContent property of this element.
        /// </summary>
        public virtual string GetJsInnerText(bool trim = true)
        {
            var obj = browser.GetJavaScriptExecutor()?.ExecuteScript(@"var a = (arguments || [{}])[0];return a['innerText'] || a['textContent'];", WebElement);
            return trim ? obj?.ToString().Trim() : obj?.ToString();
        }

        public virtual IElementWrapper CheckIfIsClickable()
        {
            bool a = IsClickable();
            if (!a)
                throw new UnexpectedElementStateException($"The element '{FullSelector}' is not clickable.");
            return this;
        }

        public virtual IElementWrapper CheckIfIsNotClickable()
        {
            bool a = IsClickable();
            if (!a)
                throw new UnexpectedElementStateException($"The element '{FullSelector}' is clickable and should not be.");
            return this;
        }

        public bool IsClickable()
        {
            var obj = this.browser.GetJavaScriptExecutor().ExecuteScript(@"
                if(arguments.length === 0) {
                    throw ""Function CheckIfIsClickable requires element in arguments."";
                }
                var elm = arguments[0];
                var rec = elm.getBoundingClientRect();

                // is not visible
                if (rec.width === 0 || rec.height === 0)
                {
                    return false;
                }
                //check if is on top

                var top = document.elementFromPoint(rec.left + (rec.width / 2), rec.top + (rec.height / 2));
                return top === elm;
            ", this.WebElement);
            var a = (bool)obj;
            return a;
        }

        /// <summary>
        /// This check-method inserts javascript to the site and checks returned value of innerText/textContent property of specific element.
        /// </summary>
        public virtual IElementWrapper CheckIfJsPropertyInnerTextEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            var value = GetJsInnerText(trim);
            if (!string.Equals(text, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains incorrect content in innerText/textContent property. Expected content: '{text}', Provided content: '{value}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// This check-method inserts javascript to the site and checks returned value of innerText/textContent property of specific element.
        /// </summary>
        public virtual IElementWrapper CheckIfJsPropertyInnerText(Func<string, bool> expression, string failureMesssage = null, bool trim = true)
        {
            var value = GetJsInnerText(trim);
            if (!expression(value))
            {
                throw new UnexpectedElementStateException($"Element contains incorrect content in innerText property of element. Provided content: '{value}' \r\n Element selector: {FullSelector} \r\n{failureMesssage ?? ""}");
            }
            return this;
        }

        /// <summary>
        /// Inserts javascript to the site and returns value of innerHTML property of this element.
        /// </summary>
        /// <remarks>Some browsers adds unneccessery attributes to InnerHtml property. Comparison of raw html strings is NOT recommended.</remarks>
        public string GetJsInnerHtml()
        {
            return GetJsElementPropertyValue("innerHTML");
        }

        /// <summary>
        /// This check-method inserts javascript to the site and checks returned value of innerHTML property of specific element.
        /// </summary>
        /// <remarks>Some browsers adds unneccessery attributes to InnerHtml property. Be sure that all browsers you are using are generating the same result to prevent unexpected results of this method.</remarks>
        public virtual IElementWrapper CheckIfJsPropertyInnerHtmlEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            var value = GetJsInnerHtml();
            if (trim)
            {
                value = value?.Trim();
            }
            if (!string.Equals(text, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains incorrect content in innerHTML property. Expected content: '{text}', Provided content: '{value}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// This check-method inserts javascript to the site and checks returned value of innerHTML property of specific element.
        /// </summary>
        /// <remarks>Some browsers adds unneccessery attributes to InnerHtml property. Be sure that all browsers you are using are generate the same result to prevent unexpected results of this method.</remarks>

        public virtual IElementWrapper CheckIfJsPropertyInnerHtml(Func<string, bool> expression, string failureMessage = null)
        {
            var value = GetJsInnerHtml();
            if (!expression(value))
            {
                throw new UnexpectedElementStateException($"Element contains incorrect content in innerHTML property of element. Provided content: '{value}' \r\n Element selector: {FullSelector} \r\n{failureMessage ?? ""}");
            }
            return this;
        }

        /// <summary>
        /// Checks name of tag
        /// </summary>
        public virtual IElementWrapper CheckTagName(Func<string, bool> expression, string failureMessage = null)
        {
            if (!expression(GetTagName()))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n { (failureMessage ?? "")}");
            }
            return this;
        }

        /// <summary>
        /// Checks name of tag
        /// </summary>
        public virtual IElementWrapper CheckIfTagName(Func<string, bool> expression, string failureMessage = null)
        {
            return CheckTagName(expression, failureMessage);
        }

        /// <param name="attributeName">write name of attribute to check</param>
        /// <param name="expression">define condition</param>
        /// <param name="failureMessage">When value of the element does not satisfy the condition this fail failureMessage is written to the throwen exception in the output.</param>
        /// <returns></returns>
        public virtual IElementWrapper CheckAttribute(string attributeName, Func<string, bool> expression, string failureMessage = null)
        {
            var attribute = WebElement.GetAttribute(attributeName);
            if (!expression(attribute))
            {
                throw new UnexpectedElementStateException($"Attribute '{attributeName}' contains unexpected value. Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n {failureMessage ?? ""}");
            }
            return this;
        }

        public virtual IElementWrapper CheckAttribute(string attributeName, string value, bool caseInsensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var attribute = WebElement.GetAttribute(attributeName);
            if (trimValue)
            {
                attribute = attribute.Trim();
                value = value.Trim();
            }
            if (!string.Equals(value, attribute,
                caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
            {
                throw new UnexpectedElementStateException(failureMessage ?? $"Attribute '{attributeName}' contains unexpected value. Expected value: '{value}', Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckAttribute(string attributeName, string[] allowedValues, bool caseInsensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var attribute = WebElement.GetAttribute(attributeName);
            if (trimValue)
            {
                attribute = attribute.Trim();
                allowedValues = allowedValues.Select(s => s.Trim()).ToArray();
            }
            if (allowedValues.All(v => !string.Equals(v, attribute,
                caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)))
            {
                throw new UnexpectedElementStateException(failureMessage ?? $"Attribute contains unexpected value. Expected value: '{string.Concat("|", allowedValues)}', Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckClassAttribute(Func<string, bool> expression, string failureMessage = "")
        {
            return CheckAttribute("class", expression, failureMessage);
        }

        public virtual IElementWrapper CheckClassAttribute(string value, bool caseInsensitive = false, bool trimValue = true)
        {
            return CheckAttribute("class", value, caseInsensitive, trimValue);
        }

        public virtual IElementWrapper CheckIfHasClass(string value, bool caseInsensitive = false)
        {
            return CheckAttribute("class", p => p.Split(' ').Any(c => string.Equals(c, value, caseInsensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public virtual IElementWrapper CheckIfHasNotClass(string value, bool caseInsensitive = false)
        {
            return CheckAttribute("class", p => !p.Split(' ').Any(c => string.Equals(c, value, caseInsensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value should not contain: '{value}'.");
        }

        public string GetAttribute(string name)
        {
            return WebElement.GetAttribute(name);
        }

        public bool HasAttribute(string name)
        {
            bool result = false;
            var obj = browser.GetJavaScriptExecutor()?.ExecuteScript("return (arguments || [{attributes:[]}])[0].attributes[\"" + name + "\"] !== undefined;", WebElement);
            bool.TryParse((obj?.ToString() ?? "false"), out result);
            return result;
        }

        public IElementWrapper CheckIfHasAttribute(string name)
        {
            if (!HasAttribute(name))
            {
                throw new UnexpectedElementStateException($"Element has not attribute '{name}'. Element selector: '{FullSelector}'.");
            }
            return this;
        }

        public IElementWrapper CheckIfHasNotAttribute(string name)
        {
            if (HasAttribute(name))
            {
                throw new UnexpectedElementStateException($"Attribute '{name}' was not expected. Element selector: '{FullSelector}'.");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfInnerTextEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (!string.Equals(text, GetInnerText(),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Expected content: '{text}', Provided content: '{GetInnerText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfInnerText(Func<string, bool> expression, string failureMessage = null)
        {
            if (!expression(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Provided content: '{GetInnerText()}' \r\n Element selector: {FullSelector} \r\n {failureMessage ?? ""}");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfTextEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (!string.Equals(text, GetTrimmedText(trim),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Expected content: '{text}', Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        private string GetTrimmedText(bool trim)
        {
            if (trim)
            {
                return GetText()?.Trim();
            }
            else
            {
                return GetText();
            }
        }

        public virtual IElementWrapper CheckIfTextNotEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (string.Equals(text, GetTrimmedText(trim),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Content cannot contain: '{text}', Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfText(Func<string, bool> expression, string failureMessage = null)
        {
            if (!expression(GetText()))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n {failureMessage ?? ""}");
            }
            return this;
        }

        /// <summary>
        /// Sets current option of element &lt;select&gt;.
        /// </summary>
        /// <param name="index">Index of  &lt;option&gt; that should be selected.</param>
        public virtual IElementWrapper Select(int index)
        {
            return Select(e => e.SelectByIndex(index));
        }

        /// <summary>
        /// Sets current option of element &lt;select&gt;.
        /// </summary>
        /// <param name="value">Value of  &lt;option&gt; that should be selected.</param>
        public virtual IElementWrapper Select(string value)
        {
            return Select(e => e.SelectByValue(value));
        }

        /// <summary>
        /// Sets current option of element &lt;select&gt;.
        /// </summary>
        public virtual IElementWrapper Select(Action<SelectElement> process)
        {
            return PerformActionOnSelectElement(process);
        }

        public virtual IElementWrapper PerformActionOnSelectElement(Action<SelectElement> process)
        {
            var selectElm = new SelectElement(WebElement);
            process(selectElm);
            Wait();
            return this;
        }

        public virtual string GetTagName()
        {
            return WebElement.TagName.Trim().ToLower(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Submits this element to the web server.
        /// </summary>
        /// <remarks>
        /// If this current element is a form, or an element within a form,
        ///             then this will be submitted to the web server. If this causes the current
        ///             page to change, then this method will block until the new page is loaded.
        /// </remarks>

        public virtual IElementWrapper Submit()
        {
            WebElement.Submit();
            Wait();
            return this;
        }

        public virtual IElementWrapper SendKeys(string text)
        {
            WebElement.SendKeys(text);
            Wait();
            return this;
        }

        /// <summary>
        /// Finds all elements that satisfy the condition of css selector.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="tmpSelectMethod"> This select method is used only for selection of elements in this query. Not in the future.</param>
        /// <returns></returns>
        public virtual IElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var collection = WebElement.FindElements((tmpSelectMethod ?? SelectMethod)(selector)).ToElementsList(browser, selector, this);
            collection.ParentWrapper = this;
            return collection;
        }

        /// <summary>
        /// Returns first element that satisfies the selector.
        /// </summary>
        public virtual IElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var elms = FindElements(selector, tmpSelectMethod);
            return elms.FirstOrDefault();
        }

        /// <summary>
        /// Returns first element that satisfies the selector. Throws exception when no element is found.
        /// </summary>
        public virtual IElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return ThrowIfIsNull(FirstOrDefault(selector, tmpSelectMethod), $"Element not found. Selector: {selector}");
        }

        /// <summary>
        /// Returns one element and throws exception when more then one element is found.
        /// </summary>
        public virtual IElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).SingleOrDefault();
        }

        /// <summary>
        /// Returns one element and throws exception when no element or more then one element is found.
        /// </summary>
        public virtual IElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).Single();
        }

        /// <summary>
        /// Returns one element from collection at specified position and throws exception when collection contains less elements then is expected.
        /// </summary>
        public virtual IElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).ElementAt(index);
        }

        /// <summary>
        /// Returns last element from collection and Throws exception when the collection is empty.
        /// </summary>
        public virtual IElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).Last();
        }

        /// <summary>
        /// Returns last element from collection or null when collection is empty.
        /// </summary>
        public virtual IElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).LastOrDefault();
        }

        /// <summary>
        /// Throws exception when provided object is null.
        /// </summary>
        public T ThrowIfIsNull<T>(T obj, string message)
        {
            if (obj == null)
            {
                throw new NoSuchElementException(message);
            }
            return obj;
        }

        public virtual string GetInnerText()
        {
            return WebElement.Text;
        }

        public virtual string GetText()
        {
            string[] valueElements = new[] { "input", "textarea" };
            if (valueElements.Contains(WebElement.TagName.Trim().ToLower()))
            {
                return GetAttribute("value");
            }
            return WebElement.Text;
        }

        public Size GetSize()
        {
            return WebElement.Size;
        }

        public virtual IElementWrapper Click()
        {
            WebElement.Click();
            Wait();
            return this;
        }

        public virtual IElementWrapper CheckIfIsDisplayed()
        {
            if (!IsDisplayed())
            {
                throw new UnexpectedElementStateException($"Element is not displayed. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfIsNotDisplayed()
        {
            if (IsDisplayed())
            {
                throw new UnexpectedElementStateException($"Element is displayed and should not be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfIsChecked()
        {
            CheckTagName("input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            if (!IsChecked())
                throw new UnexpectedElementStateException($"Element is NOT checked and should be. \r\n Element selector: {Selector} \r\n");
            return this;
        }

        public virtual IElementWrapper CheckIfIsNotChecked()
        {
            CheckTagName("input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            if (IsChecked())
            {
                throw new UnexpectedElementStateException($"Element is checked and should NOT be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

      

        public virtual IElementWrapper CheckIfValue(string value, bool caseInsensitive = false, bool trimValue = true)
        {
            string elementValue = GetValue();

            if (trimValue)
            {
                elementValue = elementValue?.Trim();
                value = value.Trim();
            }
            if (!string.Equals(value, elementValue,
                caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Expected value: '{value}', Provided value: '{elementValue}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public string GetValue()
        {
            var tag = GetTagName();
            string elementValue = null;
            //input
            if (tag == "input")
            {
                elementValue = WebElement.GetAttribute("value");
            }
            //textarea
            if (tag == "textarea")
            {
                elementValue = GetInnerText();
            }

            return elementValue;
        }

        private bool IsChecked()
        {
            return WebElement.Selected;
        }

        private bool TryParseBool(string value)
        {
            bool tmp;
            bool.TryParse(value, out tmp);
            return tmp;
        }

        public virtual IElementWrapper CheckIfIsEnabled()
        {
            if (!IsEnabled())
            {
                throw new UnexpectedElementStateException($"Element is not enabled. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public IElementWrapper CheckIfIsSelected()
        {
            if (!IsSelected())
            {
                throw new UnexpectedElementStateException($"Element is not selected. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// Returns new api to define validation rules of the element.
        /// </summary>
        public CheckElementWrapper Check()
        {
            return new CheckElementWrapper(this);
        }

        public virtual IElementWrapper CheckIfContainsText()
        {
            if (string.IsNullOrWhiteSpace(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element doesn't contain text. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual IElementWrapper CheckIfIsNotEnabled()
        {
            if (IsEnabled())
            {
                throw new UnexpectedElementStateException($"Element is enabled and should not be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public IElementWrapper CheckIfIsNotSelected()
        {
            if (IsSelected())
            {
                throw new UnexpectedElementStateException($"Element is selected and should not be.\r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// Checks whether element contains some text.
        /// </summary>
        public virtual IElementWrapper CheckIfDoesNotContainsText()
        {
            if (!string.IsNullOrWhiteSpace(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element does contain text. Element should be empty.\r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// Indicates whether element is visible.
        /// An element that has zero width or height also counts as non visible.
        /// </summary>
        public bool IsDisplayedAndHasSizeGreaterThanZero()
        {
            return WebElement.Displayed && !(WebElement.Size.Height == 0 || WebElement.Size.Width == 0);
        }

        /// <summary>
        /// Indicates whether element is visible.
        /// An element that has zero width or height also counts as non visible.
        /// </summary>
        public bool IsDisplayed()
        {
            return WebElement.Displayed;
        }

        /// <summary>
        /// Indicates whether element is selected.
        /// </summary>
        public bool IsSelected()
        {
            return WebElement.Selected;
        }

        /// <summary>
        /// Indicates whether element is enabled.
        /// </summary>
        public bool IsEnabled()
        {
            return WebElement.Enabled;
        }

        /// <summary>
        /// Removes content of element.
        /// </summary>
        public IElementWrapper Clear()
        {
            WebElement.Clear();
            Wait();
            return this;
        }

        /// <summary>
        /// Waits the ActionWaitTime before next step.
        /// </summary>
        public virtual IElementWrapper Wait()
        {
            if (ActionWaitTime != 0)
                Thread.Sleep(ActionWaitTime);
            return this;
        }

        /// <summary>
        /// Waits the specified time before next step.
        /// </summary>
        public virtual IElementWrapper Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return this;
        }

        /// <summary>
        /// Waits until the condition is true.
        /// </summary>
        /// <param name="condition">Expression that determine whether test should wait or continue</param>
        /// <param name="maxTimeout">If condition is not reached in this timeout (ms) test is dropped.</param>
        /// <param name="failureMessage">Message which is displayed in exception log in case that the condition is not reached</param>
        /// <param name="ignoreCertainException">When StaleElementReferenceException or InvalidElementStateException is thrown than it would be ignored.</param>
        /// <param name="checkInterval">Interval in milliseconds. RECOMMENDATION: let the interval greater than 250ms</param>
        public IElementWrapper WaitFor(Func<IElementWrapper, bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true, int checkInterval = 500)
        {
            if (condition == null)
            {
                throw new NullReferenceException("Condition cannot be null.");
            }
            var now = DateTime.UtcNow;

            bool isConditionMet = false;
            do
            {
                try
                {
                    isConditionMet = condition(this);
                }
                catch (StaleElementReferenceException)
                {
                    if (!ignoreCertainException)
                        throw;
                }
                catch (InvalidElementStateException)
                {
                    if (!ignoreCertainException)
                        throw;
                }

                if (DateTime.UtcNow.Subtract(now).TotalMilliseconds > maxTimeout)
                {
                    throw new WaitBlockException(failureMessage);
                }
                Wait(checkInterval);
            } while (!isConditionMet);

            return this;
        }

        public IElementWrapper WaitFor(Action<IElementWrapper> checkExpression, int maxTimeout, string failureMessage, int checkInterval = 500)
        {
            return WaitFor(elm =>
            {
                try
                {
                    checkExpression(elm);
                }
                catch
                {
                    return false;
                }
                return true;
            }, maxTimeout, failureMessage, true, checkInterval);
        }

        /// <summary>
        /// Waits the specified time before next step.
        /// </summary>
        public virtual IElementWrapper Wait(TimeSpan interval)
        {
            Thread.Sleep(interval);
            return this;
        }

        /// <summary>
        /// Checks href value of element A (hyperlink)
        /// </summary>
        /// <param name="url">Expected value of href.</param>
        /// <param name="kind">Type of url of expected href.</param>
        /// <param name="components">Determines what parts of url should be compared.</param>
        /// <returns></returns>
        public IElementWrapper CheckIfHyperLinkEquals(string url, UrlKind kind, params UriComponents[] components)
        {
            if (components.Length == 0)
            {
                components = new UriComponents[1];
                components[0] = kind == UrlKind.Relative ? UriComponents.PathAndQuery : UriComponents.AbsoluteUri;
            }

            var providedHref = new Uri(WebElement.GetAttribute("href"));
            if (kind == UrlKind.Relative)
            {
                var host = BaseUrl;
                if (string.IsNullOrWhiteSpace(host))
                {
                    host = "http://example.com/";
                }
                else if (!host.EndsWith("/"))
                {
                    host += "/";
                }
                url = host + (url.StartsWith("/") ? url.Substring(1) : url);
            }
            if (kind == UrlKind.Absolute && url.StartsWith("//"))
            {
                url = providedHref.Scheme + ":" + url;
            }
            var expectedHref = new Uri(url);
            UriComponents finalComponent = components[0];
            components.ToList().ForEach(s => finalComponent |= s);

            if (Uri.Compare(providedHref, expectedHref, finalComponent, UriFormat.SafeUnescaped,
                    StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedElementStateException($"Link '{FullSelector}' provided value '{providedHref}' of attribute href. Provided value does not match with expected value '{url}'.");
            }

            return this;
        }

        public IElementWrapper ScrollTo(IElementWrapper element)
        {
            var javascript = @"
            function findPosition(element) {
                var curtop = 0;
                if (element.offsetParent) {
                    do {
                        curtop += element.offsetTop;
                    } while (element = element.offsetParent);
                return [curtop];
                }
            }

            window.scroll(0,findPosition(arguments[0]));
        ";
            var executor = element.BrowserWrapper.GetJavaScriptExecutor();
            executor.ExecuteScript(javascript, element.WebElement);
            return this;
        }

        public IElementWrapper CheckIfIsElementInView(IElementWrapper element)
        {
            if (!IsElementInView(element))
            {
                throw new UnexpectedElementStateException($"Element is not in browser view. {element.ToString()}");
            }
            return this;
        }

        public IElementWrapper CheckIfIsElementNotInView(IElementWrapper element)
        {
            if (IsElementInView(element))
            {
                throw new UnexpectedElementStateException($"Element is in browser view. {element.ToString()}");
            }
            return this;
        }

        public bool IsElementInView(IElementWrapper element)
        {
            var executor = element.BrowserWrapper.GetJavaScriptExecutor();

            var result = executor.ExecuteScript(@"
function elementInViewport2(el) {
  var top = el.offsetTop;
  var left = el.offsetLeft;
  var width = el.offsetWidth;
  var height = el.offsetHeight;

  while(el.offsetParent) {
    el = el.offsetParent;
    top += el.offsetTop;
    left += el.offsetLeft;
  }

  return (
    top < (window.pageYOffset + window.innerHeight) &&
    left < (window.pageXOffset + window.innerWidth) &&
    (top + height) > window.pageYOffset &&
    (left + width) > window.pageXOffset
  );
}

return elementInViewport2(arguments[0]);
                ", element.WebElement);

            return (bool)result;
        }

        /// <summary>
        /// Drag this element and drop to dropToElement with offsetX and offsetY.
        /// </summary>
        /// <param name="dropToElement"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public IElementWrapper DropTo(IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0)
        {
            throw new NotImplementedException();
            //BrowserWrapper.DragAndDrop(this, dropToElement, offsetX, offsetY);
            return this;
        }

        /// <summary>
        /// Determines whether the specified CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>
        ///   <c>true</c> if has the specified CSS class otherwise, <c>false</c>.
        /// </returns>
        public bool HasCssClass(string cssClass)
        {
            var attr = GetAttribute("class");
            return attr.Split(' ').Any(s => string.Equals(cssClass, s, StringComparison.OrdinalIgnoreCase));
        }
    }
}