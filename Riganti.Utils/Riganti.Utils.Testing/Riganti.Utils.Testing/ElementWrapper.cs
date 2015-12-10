using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ElementWrapper : ISeleniumWrapper
    {
        private readonly IWebElement element;
        private readonly BrowserWrapper browser;

        public string Selector { get; set; }
        public string FullSelector => GenerateFullSelector();

        public static int ActionTimeout { get; set; } = 400;

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

        public ElementWrapper CheckTagName(string expectedTagName)
        {
            if (!string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Expected value: '{expectedTagName}', Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public string GetJsElementPropertyValue(string elementPropertyName)
        {
            var obj = browser.GetJavaScriptExecutor()?.ExecuteScript(@"return (arguments || [{}])[0]['" + elementPropertyName + "'];", WebElement);
            return obj?.ToString();
        }

        /// <summary>
        /// Inserts javascript to the site and returns value of innerText/textContent property of this element. 
        /// </summary>
        public string GetJsInnerText(bool trim = true)
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

        public string GetAttribute(string name)
        {
            return element.GetAttribute(name);
        }
        public bool HasAttribute(string name)
        {
            bool result = false;
            var obj = browser.GetJavaScriptExecutor()?.ExecuteScript("return (arguments || [{attributes:[]}])[0].attributes[\"" +  name + "\"] !== undefined;", WebElement);
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
        /// <returns></returns>
        public virtual ElementWrapperCollection FindElements(string selector)
        {
            var collection = element.FindElements(SelectMethod(selector)).ToElementsList(browser, selector, this);
            collection.ParentWrapper = this;
            return collection;
        }

        public virtual ElementWrapper FirstOrDefault(string selector)
        {
            var elms = FindElements(selector);
            return elms.FirstOrDefault();
        }

        public virtual ElementWrapper First(string selector)
        {
            return ThrowIfIsNull(FirstOrDefault(selector), $"Element not found. Selector: {selector}");
        }

        public virtual ElementWrapper SingleOrDefault(string selector)
        {
            return FindElements(selector).SingleOrDefault();
        }

        public virtual ElementWrapper Single(string selector)
        {
            return FindElements(selector).Single();
        }

        public virtual ElementWrapper ElementAt(string selector, int index)
        {
            return FindElements(selector).ElementAt(index);
        }

        public virtual ElementWrapper Last(string selector)
        {
            return FindElements(selector).Last();
        }

        public virtual ElementWrapper LastOrDefault(string selector)
        {
            return FindElements(selector).LastOrDefault();
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
            if (!IsChecked())
            {
                throw new UnexpectedElementStateException($"Element is NOT checked and should be. \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }
        public virtual ElementWrapper CheckIfIsNotChecked()
        {
            if (IsChecked())
            {
                throw new UnexpectedElementStateException($"Element is checked and should NOT be. \r\n Element selector: {Selector} \r\n");
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

        public virtual ElementWrapper CheckIfDoesNotContainsText()
        {
            if (!string.IsNullOrWhiteSpace(GetInnerText()))
            {
                throw new UnexpectedElementStateException($"Element does contain text. Element should be empty.\r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public bool IsDisplayed()
        {
            return element.Displayed;
        }

        public bool IsSelected()
        {
            return element.Selected;
        }

        public bool IsEnabled()
        {
            return element.Enabled;
        }

        public ElementWrapper Clear()
        {
            element.Clear();
            Wait();
            return this;
        }

        public virtual ElementWrapper Wait()
        {
            if (ActionWaitTime != 0)
                Thread.Sleep(ActionWaitTime);
            return this;
        }
        public virtual ElementWrapper Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return this;
        }
        public virtual ElementWrapper Wait(TimeSpan interval)
        {
            Thread.Sleep(interval);
            return this;
        }
    }


}