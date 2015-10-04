using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class BrowserWrapper
    {
        private readonly IWebDriver browser;
        public IWebDriver Browser => browser;

        public BrowserWrapper(IWebDriver browser)
        {
            this.browser = browser;
            SetCssSelector();
        }

        private Func<string, By> selectorPreprocessMethod;

        public virtual Func<string, By> SelectorPreprocessor
        {
            get { return selectorPreprocessMethod; }
            set
            {
                if (value == null)
                { throw new Exception("Wrong selector preprocess methode."); }
                selectorPreprocessMethod = value;
            }
        }

        public void SetCssSelector()
        {
            selectorPreprocessMethod = By.CssSelector;
        }

        /// <summary>
        /// Url of active browser tab.
        /// </summary>
        public string CurrentUrl => browser.Url;

        /// <summary>
        /// Gives path of url of active browser tab.
        /// </summary>
        public string CurrentUrlPath => new Uri(CurrentUrl).GetLeftPart(UriPartial.Path);

        /// <summary>
        /// Compares url with current url of browser.
        /// </summary>
        public bool CompareUrl(string url)
        {
            Uri uri1 = new Uri(url);
            Uri uri2 = new Uri(browser.Url);

            var result = Uri.Compare(uri1, uri2,
                UriComponents.Scheme | UriComponents.Host | UriComponents.PathAndQuery,
                UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);

            return result == 0;
        }

        public void Click(string selector)
        {
            First(selector).Click();
            Thread.Sleep(100);
        }

        public void Submit(string selector)
        {
            First(selector).Submit();
            Thread.Sleep(100);
        }

        public void NavigateToUrl(string url)
        {
            browser.Navigate().GoToUrl(url);
        }

        public void NavigateBack()
        {
            browser.Navigate().Back();
        }

        public void NavigateForward()
        {
            browser.Navigate().Forward();
        }

        public void Refresh()
        {
            browser.Navigate().Refresh();
        }

        public string GetAlertText()
        {
            var alert = browser.SwitchTo().Alert();
            return alert?.Text;
        }

        public void ConfirmAlert()
        {
            browser.SwitchTo().Alert().Accept();
            Thread.Sleep(500);
        }

        /// <summary>
        /// Finds all elements that satisfy the condition of css selector.
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public ElementWrapperCollection FindElements(string selector)
        {
            return browser.FindElements(SelectorPreprocessor(selector)).ToElementsList(this, selector);
        }

        public ElementWrapper FirstOrDefault(string selector)
        {
            var elms = FindElements(selector);
            return elms.FirstOrDefault();
        }

        public ElementWrapper First(string selector)
        {
            return ThrowIfIsNull(FirstOrDefault(selector), $"Element not found. Selector: {selector}");
        }

        public ElementWrapper SingleOrDefault(string selector)
        {
            return FindElements(selector).SingleOrDefault();
        }

        public ElementWrapper Single(string selector)
        {
            return FindElements(selector).Single();
        }

        public ElementWrapper ElementAt(string selector, int index)
        {
            return FindElements(selector).ElementAt(index);
        }

        public ElementWrapper Last(string selector)
        {
            return FindElements(selector).Last();
        }

        public ElementWrapper LastOrDefault(string selector)
        {
            return FindElements(selector).LastOrDefault();
        }

        public void Blur()
        {
            var jsExecutor = browser as IJavaScriptExecutor;
            jsExecutor?.ExecuteScript("if(document.activeElement && document.activeElement.blur) {document.activeElement.blur()}");
        }

        public void SendKeys(string selector, string text)
        {
            FindElements(selector).ForEach(s => s.SendKeys(text));
        }

        public void ClearElementsContent(string selector)
        {
            FindElements(selector).ForEach(s => s.Clear());
        }

        public T ThrowIfIsNull<T>(T obj, string message)
        {
            if (obj == null)
            {
                throw new NoSuchElementException(message);
            }
            return obj;
        }

        /// <summary>
        /// Takes a screenshot and returns a full path to the file.
        /// </summary>
        ///<param name="filename">Path where the screenshot is going to be saved.</param>
        ///<param name="format">Default value is PNG.</param>
        public void TakeScreenshot(string filename, ImageFormat format = null)
        {
            ((ITakesScreenshot)browser).GetScreenshot().SaveAsFile(filename, format);
        }

        public void Dispose()
        {
            browser.Dispose();
        }
    }
}