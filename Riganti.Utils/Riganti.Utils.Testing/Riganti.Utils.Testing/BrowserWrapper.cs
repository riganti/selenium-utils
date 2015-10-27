using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class BrowserWrapper
    {
        // ReSharper disable once InconsistentNaming
        protected readonly IWebDriver browser;

        public IWebDriver Browser => browser;

        public BrowserWrapper(IWebDriver browser)
        {
            this.browser = browser;
            SetCssSelector();
            var timeouts = browser.Manage().Timeouts();
            timeouts.SetPageLoadTimeout(TimeSpan.FromSeconds(15));
            timeouts.ImplicitlyWait(TimeSpan.FromMilliseconds(150));
        }

        public void SetTimeouts(TimeSpan pageLoadTimeout, TimeSpan implicitlyWait)
        {
            var timeouts = browser.Manage().Timeouts();
            timeouts.SetPageLoadTimeout(pageLoadTimeout);
            timeouts.ImplicitlyWait(implicitlyWait);
        }

        private Func<string, By> selectorPreprocessMethod;

        public virtual Func<string, By> SelectorPreprocessMethod
        {
            get { return selectorPreprocessMethod; }
            set
            {
                if (value == null)
                { throw new ArgumentNullException("Wrong selector preprocess method."); }
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
            if (string.IsNullOrWhiteSpace(url))
            {
                if (string.IsNullOrWhiteSpace(SeleniumTestsConfiguration.BaseUrl))
                {
                    throw new InvalidRedirectException();
                }
                browser.Navigate().GoToUrl(SeleniumTestsConfiguration.BaseUrl);
                return;
            }
            //redirect if is absolute
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute) || url.StartsWith("//"))
            {
                browser.Navigate().GoToUrl(url);
                return;
            }

            var builder = new UriBuilder(SeleniumTestsConfiguration.BaseUrl);

            // replace url fragments
            if (url.StartsWith("/"))
            {
                builder.Path = url;
                browser.Navigate().GoToUrl(builder.ToString());
                return;
            }
            // setup fragments (join urls)
            var path = builder.Path;
            path = path + (path.EndsWith("/") ? "" : "/");
            builder.Path = path + url;

            browser.Navigate().GoToUrl(builder.ToString());
        }

        public void NavigateToUrl()
        {
            NavigateToUrl(null);
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
        /// <param name="cssSelector"></param>
        /// <returns></returns>
        public ElementWrapperCollection FindElements(string cssSelector)
        {
            return browser.FindElements(SelectorPreprocessMethod(cssSelector)).ToElementsList(this, SelectorPreprocessMethod(cssSelector).GetSelector());
        }
        
        public BrowserWrapper Wait(int millisecconds)
        {
            Thread.Sleep(millisecconds);
            return this;
        }
        public BrowserWrapper Wait(TimeSpan interval)
        {
            Thread.Sleep(interval);
            return this;
        }
        /// <summary>
        /// Finds all elements that satisfy the condition of css selector.
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public ElementWrapperCollection FindElements(By selector)
        {
            return browser.FindElements(selector).ToElementsList(this, selector.GetSelector());
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

        public bool IsDisplayed(string selector)
        {
            return FindElements(selector).All(s => s.IsDisplayed());
        }

        public ElementWrapperCollection CheckIfIsDisplayed(string selector)
        {
            var collection = FindElements(selector);
            var result = collection.All(s => s.IsDisplayed());
            if (!result)
            {
                var index = collection.IndexOf(collection.First(s => !s.IsDisplayed()));
                throw new UnexpectedElementStateException($"One or more elements are not displayed. Selector '{selector}', Index of non-displayed element: {index}");
            }
            return collection;
        }

        public ElementWrapperCollection CheckIfIsNotDisplayed(string selector)
        {
            var collection = FindElements(selector);
            var result = collection.All(s => s.IsDisplayed()) && collection.Any();
            if (result)
            {
                var index = collection.Any() ? collection.IndexOf(collection.First(s => !s.IsDisplayed())) : -1;
                throw new UnexpectedElementStateException($"One or more elements are displayed and they shouldn't be. Selector '{selector}', Index of non-displayed element: {index}");
            }
            return collection;
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

        public void FireJsBlur()
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
            ((ITakesScreenshot)browser).GetScreenshot().SaveAsFile(filename, format ?? ImageFormat.Png);
        }

        public void Dispose()
        {
            browser.Dispose();
        }
    }
}