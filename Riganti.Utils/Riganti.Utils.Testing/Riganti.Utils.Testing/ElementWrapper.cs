﻿using OpenQA.Selenium;
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
    public class ElementWrapper : ISeleniumWrapper
    {
        private readonly IWebElement element;
        private readonly BrowserWrapper browser;

        public string Selector { get; set; }
        public string FullSelector => GenerateFullSelector();

        public int ActionWaitTime { get; set; } = 100;

        public IWebElement WebElement => element;

        public BrowserWrapper BrowserWrapper => browser;
        public ISeleniumWrapper Parent { get; set; }

        public ElementWrapper(IWebElement webElement, BrowserWrapper browserWrapper)
        {
            element = webElement;
            browser = browserWrapper;
        }

        private string GenerateFullSelector()
        {
            var parentSelector = Parent.FullSelector;
            var parent = Parent as ElementWrapperCollection;
            if (parent != null)
            {
                var index = parent.IndexOf(this);
                parentSelector = string.IsNullOrWhiteSpace(parent.FullSelector) ? "" : parent.FullSelector.Trim() + $":nth-child({index})";
                return $"{parentSelector}".Trim();
            }
            return $"{Selector}".Trim();
        }

        public ElementWrapper CheckTagName(string expectedTagName)
        {
            if (!string.Equals(GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Expected value: '{expectedTagName}', Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public ElementWrapper CheckTagName(Func<string, bool> expression)
        {
            if (!expression(GetTagName()))
            {
                throw new UnexpectedElementStateException($"Element has wrong tagName. Provided value: '{GetTagName()}' \r\n Element selector: {Selector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckAttribute(string attributeName, Func<string, bool> expression)
        {
            var attribute = element.GetAttribute(attributeName);
            if (!expression(attribute))
            {
                throw new UnexpectedElementStateException($"Attribute contains unexpected value. Provided value: '{attribute}' \r\n Element selector: {FullSelector} \r\n");
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
        public virtual ElementWrapper CheckClassAttribute(Func<string, bool> expression)
        {
            return CheckAttribute("class", expression);
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
            return element.GetAttribute(name) != null;
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
            if (!string.Equals(text, GetText(),
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Expected content: '{text}', Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n");
            }
            return this;
        }

        public virtual ElementWrapper CheckIfInnerText(string text, Func<string, bool> expression)
        {
            if (!expression(GetText()))
            {
                throw new UnexpectedElementStateException($"Element contains wrong content. Provided content: '{GetText()}' \r\n Element selector: {FullSelector} \r\n");
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
            var collection = element.FindElements(browser.SelectMethod(selector)).ToElementsList(browser, selector, this);
            collection.Parent = this;
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
            if (string.IsNullOrWhiteSpace(GetText()))
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
            if (!string.IsNullOrWhiteSpace(GetText()))
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

        public virtual void Wait()
        {
            if (ActionWaitTime != 0)
                Thread.Sleep(ActionWaitTime);
        }
    }
}