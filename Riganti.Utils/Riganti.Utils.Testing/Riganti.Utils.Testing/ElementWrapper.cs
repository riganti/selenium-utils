using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ElementWrapper : ISeleniumWrapper
    {
        public ScopeOptions CurrentScope { get; set; }
        private readonly IWebElement element;
        private readonly BrowserWrapper browser;

        public string Selector { get; set; }
        public string FullSelector => GenerateFullSelector();

        public void ActivateScope()
        {
            ParentWrapper?.ActivateScope();
        }

        public static int ActionTimeout { get; set; } = SeleniumTestsConfiguration.ActionTimeout;

        public int ActionWaitTime
        {
            get { return ActionTimeout; }
            set { ActionTimeout = value; }
        }

        public IWebElement WebElement
        {
            get
            {
                ParentWrapper.ActivateScope();
                return element;
            }
        }

        public BrowserWrapper BrowserWrapper => browser;

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

        public ElementWrapper ParentElement
        {
            get
            {
                IWebElement parent;
                try
                {
                    parent = WebElement.FindElement(By.XPath(".."));
                }
                catch (Exception ex)
                {
                    throw new NoSuchElementException($"Parent element of '{FullSelector}' was not found!", ex);
                }
                if (parent == null)
                    throw new NoSuchElementException($"Parent element of '{FullSelector}' was not found!");
                return new ElementWrapper(parent, BrowserWrapper);
            }
        }

        public ElementWrapper(IWebElement webElement, BrowserWrapper browserWrapper)
        {
            element = webElement;
            browser = browserWrapper;
            SelectMethod = browser.SelectMethod;
        }

        public void SetCssSelectMethod()
        {
            selectMethod = By.CssSelector;
        }

        public void SetBrowserSelectMethod()
        {
            selectMethod = null;
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

        public virtual ElementWrapper CheckTagName(string expectedTagName, string failureMessage = null)
        {
            if (!string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException(failureMessage ?? $"Element has wrong tagName. Expected value: '{expectedTagName}', Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfTagName(string expectedTagName, string failureMessage = null)
        {
            return CheckTagName(expectedTagName, failureMessage);
        }

        public virtual ElementWrapper CheckIfContainsElement(string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            if (FindElements(cssSelector, tmpSelectMethod).Count == 0)
            {
                throw new EmptySequenceException($"This element ('{FullSelector}') does not contain child selectable by '{cssSelector}'.");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfNotContainsElement(string cssSelector, Func<string, By> tmpSelectMethod = null)
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
        public virtual ElementWrapper SetJsElementProperty(string propertyName, object propertyValue)
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

        /// <summary>
        /// This check-method inserts javascript to the site and checks returned value of innerText/textContent property of specific element.
        /// </summary>
        public virtual ElementWrapper CheckIfJsPropertyInnerTextEquals(string text, bool caseSensitive = true, bool trim = true)
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
        public virtual ElementWrapper CheckIfJsPropertyInnerText(Func<string, bool> expression, string messsage = null, bool trim = true)
        {
            var value = GetJsInnerText(trim);
            if (!expression(value))
            {
                throw new UnexpectedElementStateException($"Element contains incorrect content in innerText property of element. Provided content: '{value}' \r\n Element selector: {FullSelector} \r\n{messsage ?? ""}");
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
        public virtual ElementWrapper CheckIfJsPropertyInnerHtmlEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            var value = GetJsInnerHtml();
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

        public virtual ElementWrapper CheckIfJsPropertyInnerHtml(Func<string, bool> expression, string messsage = null)
        {
            var value = GetJsInnerHtml();
            if (!expression(value))
            {
                throw new UnexpectedElementStateException($"Element contains incorrect content in innerHTML property of element. Provided content: '{value}' \r\n Element selector: {FullSelector} \r\n{messsage ?? ""}");
            }
            return this;
        }

        /// <summary>
        /// Checks name of tag
        /// </summary>
        public ElementWrapper CheckTagName(Func<string, bool> expression, string message = null)
        {
            if (!expression(GetTagName()))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n { (message ?? "")}");
            }
            return this;
        }

        /// <summary>
        /// Checks name of tag
        /// </summary>
        public ElementWrapper CheckIfTagName(Func<string, bool> expression, string message = null)
        {
            return CheckTagName(expression, message);
        }

        public virtual ElementWrapper CheckAttribute(string attributeName, Func<string, bool> expression, string message = null)
        {
            var attribute = WebElement.GetAttribute(attributeName);
            if (!expression(attribute))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n {message ?? ""}");
            }
            return this;
        }

        public virtual ElementWrapper CheckAttribute(string attributeName, string value, bool caseInsensitive = false, bool trimValue = true, string failureMessage = null)
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
                throw new UnexpectedElementStateException(failureMessage ?? $"Attribute contains unexpected value. Expected value: '{value}', Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckAttribute(string attributeName, string[] allowedValues, bool caseInsensitive = false, bool trimValue = true, string failureMessage = null)
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

        public virtual ElementWrapper CheckClassAttribute(Func<string, bool> expression, string messsage = "")
        {
            return CheckAttribute("class", expression, messsage);
        }

        public virtual ElementWrapper CheckClassAttribute(string value, bool caseInsensitive = false, bool trimValue = true)
        {
            return CheckAttribute("class", value, caseInsensitive, trimValue);
        }

        public virtual ElementWrapper CheckIfHasClass(string value, bool caseInsensitive = false)
        {
            return CheckAttribute("class", p => p.Split(' ').Any(c => string.Equals(c, value, caseInsensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)));
        }

        public virtual ElementWrapper CheckIfHasNotClass(string value, bool caseInsensitive = false)
        {
            return CheckAttribute("class", p => !p.Split(' ').Any(c => string.Equals(c, value, caseInsensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)));
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

        public ElementWrapper CheckIfHasAttribute(string name)
        {
            if (!HasAttribute(name))
            {
                throw new UnexpectedElementStateException($"Element has not attribute '{name}'. Element selector: '{FullSelector}'.");
            }
            return this;
        }

        public ElementWrapper CheckIfHasNotAttribute(string name)
        {
            if (HasAttribute(name))
            {
                throw new UnexpectedElementStateException($"Attribute '{name}' was not expected. Element selector: '{FullSelector}'.");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfInnerTextEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (!string.Equals(text, GetInnerText(),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Expected content: '{text}', Provided content: '{GetInnerText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfInnerText(Func<string, bool> expression, string messsage = null)
        {
            if (!expression(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Provided content: '{GetInnerText()}' \r\n Element selector: {FullSelector} \r\n {messsage ?? ""}");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfTextEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (!string.Equals(text, GetText(),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Expected content: '{text}', Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfTextNotEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (string.Equals(text, GetText(),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Content cannot contain: '{text}', Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfText(Func<string, bool> expression, string messsage = null)
        {
            if (!expression(GetText()))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n {messsage ?? ""}");
            }
            return this;
        }

        /// <summary>
        /// Sets current option of element &lt;select&gt;.
        /// </summary>
        /// <param name="index">Index of  &lt;option&gt; that should be selected.</param>
        public virtual ElementWrapper Select(int index)
        {
            return Select(e => e.SelectByIndex(index));
        }

        /// <summary>
        /// Sets current option of element &lt;select&gt;.
        /// </summary>
        /// <param name="value">Value of  &lt;option&gt; that should be selected.</param>
        public virtual ElementWrapper Select(string value)
        {
            return Select(e => e.SelectByValue(value));
        }

        /// <summary>
        /// Sets current option of element &lt;select&gt;.
        /// </summary>
        public virtual ElementWrapper Select(Action<SelectElement> process)
        {
            return PerformActionOnSelectElement(process);
        }

        public virtual ElementWrapper PerformActionOnSelectElement(Action<SelectElement> process)
        {
            var selectElm = new SelectElement(WebElement);
            process(selectElm);
            Wait();
            return this;
        }

        public virtual string GetTagName()
        {
            return WebElement.TagName.ToLower(CultureInfo.InvariantCulture).Trim().ToLower();
        }

        /// <summary>
        /// Submits this element to the web server.
        /// </summary>
        /// <remarks>
        /// If this current element is a form, or an element within a form,
        ///             then this will be submitted to the web server. If this causes the current
        ///             page to change, then this method will block until the new page is loaded.
        /// </remarks>

        public virtual ElementWrapper Submit()
        {
            WebElement.Submit();
            Wait();
            return this;
        }

        public virtual void SendKeys(string text)
        {
            WebElement.SendKeys(text);
            Wait();
        }

        /// <summary>
        /// Finds all elements that satisfy the condition of css selector.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="tmpSelectMethod"> This select method is used only for selection of elements in this query. Not in the future.</param>
        /// <returns></returns>
        public virtual ElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var collection = WebElement.FindElements((tmpSelectMethod ?? SelectMethod)(selector)).ToElementsList(browser, selector, this);
            collection.ParentWrapper = this;
            return collection;
        }

        /// <summary>
        /// Returns first element that satisfies the selector.
        /// </summary>
        public virtual ElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var elms = FindElements(selector, tmpSelectMethod);
            return elms.FirstOrDefault();
        }

        /// <summary>
        /// Returns first element that satisfies the selector. Throws exception when no element is found.
        /// </summary>
        public virtual ElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return ThrowIfIsNull(FirstOrDefault(selector, tmpSelectMethod), $"Element not found. Selector: {selector}");
        }

        /// <summary>
        /// Returns one element and throws exception when more then one element is found.
        /// </summary>
        public virtual ElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).SingleOrDefault();
        }

        /// <summary>
        /// Returns one element and throws exception when no element or more then one element is found.
        /// </summary>
        public virtual ElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).Single();
        }

        /// <summary>
        /// Returns one element from collection at specified position and throws exception when collection contains less elements then is expected.
        /// </summary>
        public virtual ElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).ElementAt(index);
        }

        /// <summary>
        /// Returns last element from collection and Throws exception when the collection is empty.
        /// </summary>
        public virtual ElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).Last();
        }

        /// <summary>
        /// Returns last element from collection or null when collection is empty.
        /// </summary>
        public virtual ElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
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

        public Size GetSize(string cssSelector)
        {
            return WebElement.Size;
        }

        public virtual ElementWrapper Click()
        {
            WebElement.Click();
            Wait();
            return this;
        }

        public virtual ElementWrapper CheckIfIsDisplayed()
        {
            if (!IsDisplayed())
            {
                throw new UnexpectedElementStateException($"Element is not displayed. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfIsNotDisplayed()
        {
            if (IsDisplayed())
            {
                throw new UnexpectedElementStateException($"Element is displayed and should not be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfIsChecked()
        {
            CheckTagName("input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            if (!IsChecked())
                throw new UnexpectedElementStateException($"Element is NOT checked and should be. \r\n Element selector: {Selector} \r\n");
            return this;
        }

        public virtual ElementWrapper CheckIfIsNotChecked()
        {
            CheckTagName("input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            if (IsChecked())
            {
                throw new UnexpectedElementStateException($"Element is checked and should NOT be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfValue(string value, bool caseInsensitive = false, bool trimValue = true)
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

        public virtual ElementWrapper CheckIfIsEnabled()
        {
            if (!IsEnabled())
            {
                throw new UnexpectedElementStateException($"Element is not enabled. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public ElementWrapper CheckIfIsSelected()
        {
            if (!IsSelected())
            {
                throw new UnexpectedElementStateException($"Element is not selected. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfContainsText()
        {
            if (string.IsNullOrWhiteSpace(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element doesn't contain text. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfIsNotEnabled()
        {
            if (IsEnabled())
            {
                throw new UnexpectedElementStateException($"Element is enabled and should not be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public ElementWrapper CheckIfIsNotSelected()
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
        public virtual ElementWrapper CheckIfDoesNotContainsText()
        {
            if (!string.IsNullOrWhiteSpace(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element does contain text. Element should be empty.\r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        /// <summary>
        /// Indicates whether element is visible.
        /// An element that has zero width and height also counts as non visible.
        /// </summary>
        public bool IsDisplayed()
        {
            return WebElement.Displayed && WebElement.Size!=new Size(0,0);
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
        public ElementWrapper Clear()
        {
            WebElement.Clear();
            Wait();
            return this;
        }

        /// <summary>
        /// Waits the ActionWaitTime before next step.
        /// </summary>
        public virtual ElementWrapper Wait()
        {
            if (ActionWaitTime != 0)
                Thread.Sleep(ActionWaitTime);
            return this;
        }

        /// <summary>
        /// Waits the specified time before next step.
        /// </summary>
        public virtual ElementWrapper Wait(int milliseconds)
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
        public ElementWrapper WaitFor(Func<ElementWrapper, bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true)
        {
            if (condition == null)
            {
                throw new NullReferenceException("Condition cannot be null.");
            }
            var now = DateTime.UtcNow;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            try
            {
                while (!condition(this))
                {
                    if (DateTime.UtcNow.Subtract(now).TotalMilliseconds > maxTimeout)
                    {
                        throw new SeleniumTestFailedException(failureMessage);
                    }
                    Wait(200);
                }
            }
            catch (StaleElementReferenceException ex)
            {
                if (!ignoreCertainException)
                    throw;
            }
            catch (InvalidElementStateException ex)
            {
                if (!ignoreCertainException)
                    throw;
            }
            return this;
        }

        /// <summary>
        /// Waits the specified time before next step.
        /// </summary>
        public virtual ElementWrapper Wait(TimeSpan interval)
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
        public ElementWrapper CheckIfHyperLinkEquals(string url, UrlKind kind, params UriComponents[] components)
        {
            if (components.Length == 0)
            {
                components = new UriComponents[1];
                components[0] = kind == UrlKind.Relative ? UriComponents.PathAndQuery : UriComponents.AbsoluteUri;
            }

            var providedHref = new Uri(WebElement.GetAttribute("href"));
            if (kind == UrlKind.Relative)
            {
                var host = SeleniumTestsConfiguration.BaseUrl;
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
    }
}