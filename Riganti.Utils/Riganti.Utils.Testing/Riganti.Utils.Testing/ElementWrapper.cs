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
        private readonly IWebElement element;
        private readonly BrowserWrapper browser;

        public string Selector { get; set; }
        public string FullSelector => GenerateFullSelector();

        public static int ActionTimeout { get; set; } = SeleniumTestsConfiguration.ActionTimeout;

        public int ActionWaitTime
        {
            get { return ActionTimeout; }
            set { ActionTimeout = value; }
        }

        public IWebElement WebElement => element;

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
                    parent = element.FindElement(By.XPath(".."));
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

        public virtual ElementWrapper CheckTagName(string expectedTagName)
        {
            if (!string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Expected value: '{expectedTagName}', Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
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
            var obj = browser.GetJavaScriptExecutor()?.ExecuteScript(@"var ________xcaijciajsicjaisjciasjicjasicjaijcias______ = (arguments || [{}])[0];return ________xcaijciajsicjaisjciasjicjasicjaijcias______['innerText'] || ________xcaijciajsicjaisjciasjicjasicjaijcias______['textContent'];", WebElement);
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
        public string GetJsInnerHtml()
        {
            return GetJsElementPropertyValue("innerHTML");
        }

        /// <summary>
        /// This check-method inserts javascript to the site and checks returned value of innerHTML property of specific element.
        /// </summary>
        /// <remarks>Some browsers adds unneccessery attributes to InnerHtml property. Be sure that all browsers you are using are generate the same result to prevent unexpected results of this method.</remarks>
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

        public ElementWrapper CheckTagName(Func<string, bool> expression, string message = null)
        {
            if (!expression(GetTagName()))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n { (message ?? "")}");
            }
            return this;
        }

        public virtual ElementWrapper CheckAttribute(string attributeName, Func<string, bool> expression, string message = null)
        {
            var attribute = element.GetAttribute(attributeName);
            if (!expression(attribute))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n {message ?? ""}");
            }
            return this;
        }

        public virtual ElementWrapper CheckAttribute(string attributeName, string value, bool caseInsensitive = false, bool trimValue = true)
        {
            var attribute = element.GetAttribute(attributeName);
            if (trimValue)
            {
                attribute = attribute.Trim();
                value = value.Trim();
            }
            if (!string.Equals(value, attribute,
                caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Expected value: '{value}', Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n");
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
            return element.GetAttribute(name);
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

        public virtual ElementWrapper Select(int index)
        {
            var selectElm = new SelectElement(element);
            selectElm.SelectByIndex(index);
            Wait();
            return this;
        }

        public virtual ElementWrapper Select(string value)
        {
            var selectElm = new SelectElement(element);
            selectElm.SelectByValue(value);
            Wait();
            return this;
        }

        public virtual ElementWrapper Select(Action<SelectElement> process)
        {
            var selectElm = new SelectElement(element);
            process(selectElm);
            Wait();
            return this;
        }

        public virtual ElementWrapper PerformActionOnSelectElement(Action<SelectElement> process)
        {
            var selectElm = new SelectElement(element);
            process(selectElm);
            Wait();
            return this;
        }

        public virtual string GetTagName()
        {
            return element.TagName.ToLower(CultureInfo.InvariantCulture).Trim().ToLower();
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
            element.Submit();
            Wait();
            return this;
        }

        public virtual void SendKeys(string text)
        {
            element.SendKeys(text);
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
            var collection = element.FindElements((tmpSelectMethod ?? SelectMethod)(selector)).ToElementsList(browser, selector, this);
            collection.ParentWrapper = this;
            return collection;
        }

        public virtual ElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var elms = FindElements(selector, tmpSelectMethod);
            return elms.FirstOrDefault();
        }

        public virtual ElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return ThrowIfIsNull(FirstOrDefault(selector, tmpSelectMethod), $"Element not found. Selector: {selector}");
        }

        public virtual ElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).SingleOrDefault();
        }

        public virtual ElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).Single();
        }

        public virtual ElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).ElementAt(index);
        }

        public virtual ElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).Last();
        }

        public virtual ElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return FindElements(selector, tmpSelectMethod).LastOrDefault();
        }

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
            return element.Text;
        }

        public virtual string GetText()
        {
            string[] valueElements = new[] { "input", "textarea" };
            if (valueElements.Contains(element.TagName.Trim().ToLower()))
            {
                return GetAttribute("value");
            }
            return element.Text;
        }

        public Size GetSize(string cssSelector)
        {
            return element.Size;
        }

        public virtual ElementWrapper Click()
        {
            element.Click();
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
            CheckTagName("input");
            CheckAttribute("type", "checkbox");

            if (!this.IsChecked())
               throw new UnexpectedElementStateException(string.Format("Element is NOT checked and should be. \r\n Element selector: {0} \r\n", (object)this.Selector));
             return this;
    }

        public virtual ElementWrapper CheckIfIsNotChecked()
        {
            CheckTagName("input");
            CheckAttribute("type", "checkbox");

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
                elementValue = element.GetAttribute("value");
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
            return TryParseBool(element.GetAttribute("checked"));
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
        /// </summary>
        public bool IsDisplayed()
        {
            return element.Displayed;
        }
        /// <summary>
        /// Indicates whether element is selected.
        /// </summary>
        public bool IsSelected()
        {
            return element.Selected;
        }
        /// <summary>
        /// Indicates whether element is enabled.
        /// </summary>
        public bool IsEnabled()
        {
            return element.Enabled;
        }
        /// <summary>
        /// Removes content of element.
        /// </summary>
        public ElementWrapper Clear()
        {
            element.Clear();
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
        public ElementWrapper WaitFor(Func< ElementWrapper, bool> condition, int maxTimeout, string failureMessage)
        {
            if (condition == null)
            {
                throw new NullReferenceException("Condition cannot be null.");
            }
            var now = DateTime.UtcNow;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (!condition(this))
            {
                if (DateTime.UtcNow.Subtract(now).TotalMilliseconds > maxTimeout)
                {
                    throw new SeleniumTestFailedException(failureMessage);
                }
                Wait(100);
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
