using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ElementWrapper
    {
        private readonly IWebElement element;
        private readonly BrowserWrapper browser;
        public int ActionWaitTime { get; set; } = 100;

        public IWebElement WebElement => element;

        public BrowserWrapper BrowserWrapper => browser;

        public ElementWrapper(IWebElement webElement, BrowserWrapper browserWrapper)
        {
            element = webElement;
            browser = browserWrapper;
        }

        public ElementWrapper CheckTagName(string expectedTagName)
        {
            if (string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Expected value: '{expectedTagName}', Provided value: '{GetTagName()}'");
            }
            return this;
        }

        public ElementWrapper CheckTagName(Func<string, bool> expression)
        {
            if (expression(GetTagName()))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Provided value: '{GetTagName()}'");
            }
            return this;
        }

        public ElementWrapper CheckAttribute(string attributeName, Func<string, bool> expression)
        {
            var attribute = element.GetAttribute(attributeName);
            if (!expression(attribute))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Provided value: '{attribute}'");
            }
            return this;
        }

        public string GetAttribute(string name)
        {
            return element.GetAttribute(name);
        }

        public virtual ElementWrapper CheckIfInnerTextEquals(string text, bool caseSensitive = true, bool trim = true)
        {
            if (!string.Equals(text, GetText(),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Expected content: '{text}', Provided content: '{GetText()}'");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfInnerText(string text, Func<string, bool> expression)
        {
            if (!expression(GetText()))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Provided content: '{GetText()}'");
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
            browser.Blur();
            Wait();
            return this;
        }

        public virtual ElementWrapper Select(Action<SelectElement> process)
        {
            var selectElm = new SelectElement(element);
            process(selectElm);
            browser.Blur();
            Wait();
            return this;
        }

        public virtual ElementWrapper PerformActionOnSelectElement(Action<SelectElement> process)
        {
            var selectElm = new SelectElement(element);
            process(selectElm);
            browser.Blur();
            Wait();
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
            if (string.Equals(value, attributeName,
                caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Expected value: '{value}', Provided value: '{attribute}'");
            }
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
            return element.FindElements(By.CssSelector(selector)).ToElementsList(browser, selector, this);
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

        public virtual string GetText()
        {
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
                throw new UnexpectedElementStateException("Element is not displayed.");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfIsEnabled()
        {
            if (!IsEnabled())
            {
                throw new UnexpectedElementStateException("Element is not enabled.");
            }
            return this;
        }

        public ElementWrapper CheckIfIsSelected()
        {
            if (!IsDisplayed())
            {
                throw new UnexpectedElementStateException("Element is not selected.");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfContainsText()
        {
            if (string.IsNullOrWhiteSpace(GetText()))
            {
                throw new UnexpectedElementStateException("Element doesn't contain text.");
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

        public virtual void Wait()
        {
            if (ActionWaitTime != 0)
                Thread.Sleep(ActionWaitTime);
        }
    }
}