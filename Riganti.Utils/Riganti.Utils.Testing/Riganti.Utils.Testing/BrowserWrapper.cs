using OpenQA.Selenium;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class BrowserWrapper
    {
        // ReSharper disable once InconsistentNaming
        protected readonly IWebDriver browser;

        private readonly ITestBase testClass;

        public IWebDriver Browser => browser;
        public int ActionWaitTime { get; set; } = 100;

        public BrowserWrapper(IWebDriver browser, ITestBase testClass)
        {
            this.browser = browser;
            this.testClass = testClass;
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

        private Func<string, By> selectMethodFunc;

        [Obsolete]
        public virtual Func<string, By> SelectorPreprocessMethod
        {
            get { return SelectMethod; }
            set
            {
                SelectMethod = value;
            }
        }

        public virtual Func<string, By> SelectMethod
        {
            get { return selectMethodFunc; }
            set
            {
                if (value == null)
                { throw new ArgumentException("SelectMethod cannot be null. This method is used to select elements from loaded page."); }
                selectMethodFunc = value;
            }
        }

        public void SetCssSelector()
        {
            selectMethodFunc = By.CssSelector;
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

            var navigateUrl = builder.ToString();
            browser.Navigate().GoToUrl(navigateUrl);
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

        public void DropTest(string message)
        {
            throw new WebDriverException($"Test forcibly dropped: {message}");
        }

        public string GetAlertText()
        {
            var alert = GetAlert();
            return alert?.Text;
        }
        public BrowserWrapper CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            var alert = GetAlert();
            var alertText = "";
            if (trim)
            {
                alertText = alert.Text?.Trim();
                expectedValue = expectedValue.Trim();
            }

            if (!string.Equals(alertText, expectedValue,
                    caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw new AlertException($"Alert does not contain expected value. Expected value: '{expectedValue}', provided value: '{alertText}'");
            }
            return this;
        }

        public IAlert GetAlert()
        {
            IAlert alert;
            try

            {
                alert = browser.SwitchTo().Alert();
            }
            catch (Exception ex)
            {
                throw new AlertException("Alert not visible.", ex);
            }
            if (alert == null)
                throw new AlertException("Alert not visible.");
            return alert;

        }

        public BrowserWrapper CheckIfAlertTextContains(string expectedValue, bool trim = true)
        {
            var alert = GetAlert();
            var alertText = "";
            if (trim)
            {
                alertText = alert.Text?.Trim();
                expectedValue = expectedValue.Trim();
            }

            if (alertText == null || !alertText.Contains(expectedValue))
            {
                throw new AlertException($"Alert does not contain expected value. Expected value: '{expectedValue}', provided value: '{alertText}'");
            }
            return this;
        }
        public BrowserWrapper CheckIfAlertText(Func<string, bool> expression, string message = "")
        {
            var alert = browser.SwitchTo().Alert()?.Text;
            if (!expression(alert))
            {
                throw new AlertException($"Alert text is not correct. Provided value: '{alert}' \n { message } ");
            }
            return this;
        }

        public BrowserWrapper ConfirmAlert()
        {
            browser.SwitchTo().Alert().Accept();
            Thread.Sleep(ActionWaitTime);
            return this;
        }
        public BrowserWrapper DismissAlert()
        {
            browser.SwitchTo().Alert().Dismiss();
            Thread.Sleep(ActionWaitTime);
            return this;
        }


        /// <summary>
        /// Finds all elements that satisfy the condition of css selector.
        /// </summary>
        /// <param name="cssSelector"></param>
        /// <returns></returns>
        public ElementWrapperCollection FindElements(string cssSelector)
        {
            return browser.FindElements(SelectMethod(cssSelector)).ToElementsList(this, SelectMethod(cssSelector).GetSelector());
        }

        public BrowserWrapper Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return this;
        }

        public BrowserWrapper Wait()
        {
            return Wait(ActionWaitTime);
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

        #region CheckUrl

        //public BrowserWrapper CheckUrlEquals(string url, params UriComponents[] criteria)
        //{
        //    UriComponents? finalCriteria = UriComponents.AbsoluteUri;

        //    if (criteria.Any())
        //    {
        //        finalCriteria = criteria[0];
        //        if (criteria.Length > 1)
        //        {
        //            for (int i = 1; i < criteria.Length; i++)
        //            {
        //                finalCriteria = finalCriteria | criteria[i];
        //            }
        //        }
        //    }
        //    var uri1 = new Uri(CurrentUrl, UriKind.RelativeOrAbsolute);
        //    var uri2 = new Uri(url, UriKind.RelativeOrAbsolute);
        //    if (Uri.Compare(uri1, uri2, (UriComponents)finalCriteria, UriFormat.Unescaped, StringComparison.OrdinalIgnoreCase) != 0)
        //    {
        //        throw new BrowserLocationException($"Current url is not expected. Current url: '{CurrentUrl}', Expected url: '{url}'.");
        //    }

        //    return this;
        //}





        public BrowserWrapper CheckUrl(Func<string, bool> expression, string message = null)
        {
            if (!expression(CurrentUrl))
            {
                throw new BrowserLocationException($"Current url is not expected. Current url: '{CurrentUrl}'. " + (message ?? ""));
            }
            return this;
        }

        #endregion CheckUrl


        #region FileUploadDialog

        /// <summary>
        /// Opens file dialog and sends keys with full path to file, that should be uploaded.
        /// </summary>
        /// <param name="fileUploadOpener">Element that opens file dialog after it is clicked.</param>
        /// <param name="fullFileName">Full path to file that is intended to be uploaded.</param>
        public virtual BrowserWrapper FileUploadDialogSelect(ElementWrapper fileUploadOpener, string fullFileName)
        {
            // open file dialog 
            fileUploadOpener.Click();

            // write the full path to the dialog
            System.Windows.Forms.SendKeys.SendWait(fullFileName);
            SendEnterKey();
            Wait();
            return this;
        }

        public virtual void SendEnterKey()
        {
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
        }
        public virtual void SendEscKey()
        {
            System.Windows.Forms.SendKeys.SendWait("{ESC}");
        }
        #endregion


        #region Frames support

        internal void CreateFrameScope(string selector)
        {
            //TODO
        }


        #endregion
    }
}