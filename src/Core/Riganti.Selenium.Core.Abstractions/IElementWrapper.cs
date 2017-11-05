using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Riganti.Selenium.Core.Abstractions
{
    public interface IElementWrapper : ISeleniumWrapper, ISupportedByValidator
    {
        int ActionWaitTime { get; set; }
        new string Selector { get; set; }
        string BaseUrl { get; set; }
        IBrowserWrapper BrowserWrapper { get; }
        IElementWrapperCollection Children { get; }
        IElementWrapper ParentElement { get; }
        Func<string, By> SelectMethod { get; set; }
        IWebElement WebElement { get; }

        IElementWrapper Clear();
        IElementWrapper Click();
        IElementWrapper DropTo(IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0);
        IElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null);
        IElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        string GetAttribute(string name);
        string GetInnerText();
        string GetJsElementPropertyValue(string elementPropertyName);
        string GetJsInnerHtml();
        string GetJsInnerText(bool trim = true);
        string GetTagName();
        string GetText();
        string GetValue();
        bool HasAttribute(string name);
        bool HasCssClass(string cssClass);
        bool IsClickable();
        bool IsDisplayed();
        bool IsDisplayedAndHasSizeGreaterThanZero();
        bool IsElementInView(IElementWrapper element);
        bool IsEnabled();
        bool IsSelected();
        Size GetSize();
        IElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper PerformActionOnSelectElement(Action<SelectElement> process);
        IElementWrapper ScrollTo(IElementWrapper element);
        IElementWrapper Select(Action<SelectElement> process);
        IElementWrapper Select(int index);
        IElementWrapper Select(string value);
        IElementWrapper SendEnterKey();
        IElementWrapper SendKeys(string text);
        void SetBrowserSelectMethod();
        void SetCssSelectMethod();
        IElementWrapper SetJsElementProperty(string propertyName, object propertyValue);
        IElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper Submit();
        T ThrowIfIsNull<T>(T obj, string message);
        IElementWrapper Wait();
        IElementWrapper Wait(int milliseconds);
        IElementWrapper Wait(TimeSpan interval);
        IElementWrapper WaitFor(Action<IElementWrapper> checkExpression, int maxTimeout, string failureMessage, int checkInterval = 500);
        IElementWrapper WaitFor(Func<IElementWrapper, bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true, int checkInterval = 500);
    }
}