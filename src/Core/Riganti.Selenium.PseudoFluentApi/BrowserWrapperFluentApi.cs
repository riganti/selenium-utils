using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Api;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Validators.Checkers.BrowserWrapperCheckers;
using Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers;

namespace Riganti.Selenium.Core
{
    public class BrowserWrapperFluentApi : BrowserWrapper, IBrowserWrapperFluentApi
    {


        /// <summary>
        /// Compares url with current url of browser.
        /// </summary>
        public bool CompareUrl(string url)
        {
            Uri uri1 = new Uri(url);
            Uri uri2 = new Uri(Driver.Url);

            var result = Uri.Compare(uri1, uri2,
                UriComponents.Scheme | UriComponents.Host | UriComponents.PathAndQuery,
                UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);

            return result == 0;
        }

        /// <summary>
        /// Compates current Url and given url.
        /// </summary>
        /// <param name="url">This url is compared with CurrentUrl.</param>
        /// <param name="urlKind">Determine whether url parameter contains relative or absolute path.</param>
        /// <param name="components">Determine what parts of urls are compared.</param>
        public bool CompareUrl(string url, UrlKind urlKind, params UriComponents[] components)
        {
            return new UrlValidator(url, urlKind, components).CompareUrl(CurrentUrl);
        }







        public IBrowserWrapperFluentApi CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<AlertException>(
                new AlertTextEqualsValidator(expectedValue, caseSensitive, trim));
        }


        /// <summary>
        /// Checks if modal dialog (Alert) contains specified text as a part of provided text from the dialog.
        /// </summary>
        public IBrowserWrapperFluentApi CheckIfAlertTextContains(string expectedValue, bool trim = true)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<AlertException>(new AlertTextContainsValidator(expectedValue, trim));

        }

        /// <summary>
        /// Checks if modal dialog (Alert) text equals with specified text.
        /// </summary>
        public IBrowserWrapperFluentApi CheckIfAlertText(Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<AlertException>(new AlertTextValidator(expression));
        }




        /// <param name="tmpSelectMethod">temporary method which determine how the elements are selected</param>

        public IElementWrapperCollection<IElementWrapperFluentApi, IBrowserWrapperFluentApi> CheckIfIsDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var collection = FindElements(selector, tmpSelectMethod);

            var validator = new IsDisplayedValidator();

            var runner = new AllOperationRunner<IElementWrapper>(collection, null);
            runner.Evaluate<UnexpectedElementStateException>(validator);

            return collection;
        }

        ///<summary>Provides elements that satisfies the selector condition at specific position.</summary>
        /// <param name="tmpSelectMethod">temporary method which determine how the elements are selected</param>
        public IElementWrapperCollection<IElementWrapperFluentApi, IBrowserWrapperFluentApi> CheckIfIsNotDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var collection = FindElements(selector, tmpSelectMethod);

            var validator = new IsNotDisplayedValidator();

            var runner = new AllOperationRunner<IElementWrapper>(collection, null);
            runner.Evaluate<UnexpectedElementStateException>(validator);

            return collection;
        }


        #region Url

        /// <summary>
        /// Checks exact match with CurrentUrl
        /// </summary>
        /// <param name="url">This url is compared with CurrentUrl.</param>
        public IBrowserWrapperFluentApi CheckUrlEquals(string url)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserLocationException>(new UrlEqualsValidator(url));
        }

        /// <summary>
        /// Checks if CurrentUrl satisfies the condition defined by lamda expression
        /// </summary>
        /// <param name="expression">The condition</param>
        /// <param name="failureMessage">Failure message</param>
        public IBrowserWrapperFluentApi CheckUrl(Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserLocationException>(new CurrentUrlValidator(expression, failureMessage));
        }

        /// <summary>
        /// Checks url by its parts
        /// </summary>
        /// <param name="url">This url is compared with CurrentUrl.</param>
        /// <param name="urlKind">Determine whether url parameter contains relative or absolute path.</param>
        /// <param name="components">Determine what parts of urls are compared.</param>
        public IBrowserWrapperFluentApi CheckUrl(string url, UrlKind urlKind, params UriComponents[] components)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserLocationException>(new UrlValidator(url, urlKind, components));
        }

        #endregion Url

        #region FileUploadDialog

        /// <summary>
        /// Opens file dialog and sends keys with full path to file, that should be uploaded.
        /// </summary>
        /// <param name="fileUploadOpener">Element that opens file dialog after it is clicked.</param>
        /// <param name="fullFileName">Full path to file that is intended to be uploaded.</param>
        public virtual IBrowserWrapperFluentApi FileUploadDialogSelect(IElementWrapper fileUploadOpener, string fullFileName)
        {
            try
            {
                OpenInputFileDialog(fileUploadOpener, fullFileName);
            }
            catch (UnexpectedElementException)
            {
#if net461
                base.OpenFileDialog(fileUploadOpener, fullFileName);
#else
                throw;
#endif
            }

            return this;
        }

        public new IElementWrapperCollection<IElementWrapperFluentApi, IBrowserWrapperFluentApi> FindElements(By selector) => base.FindElements(selector).Convert<IElementWrapperFluentApi, IBrowserWrapperFluentApi>();

        public new IElementWrapperCollection<IElementWrapperFluentApi, IBrowserWrapperFluentApi> FindElements(string cssSelector,
            Func<string, By> tmpSelectMethod = null) => base.FindElements(cssSelector, tmpSelectMethod).Convert<IElementWrapperFluentApi, IBrowserWrapperFluentApi>();

        public new IBrowserWrapperFluentApi ClearElementsContent(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IBrowserWrapperFluentApi)base.ClearElementsContent(selector, tmpSelectMethod);
        }

        public new IBrowserWrapperFluentApi Click(string selector)
        {
            return (IBrowserWrapperFluentApi)base.Click(selector);
        }

        public new IBrowserWrapperFluentApi ConfirmAlert()
        {
            return (IBrowserWrapperFluentApi)base.ConfirmAlert();
        }

        public new IBrowserWrapperFluentApi DismissAlert()
        {
            return (IBrowserWrapperFluentApi)base.DismissAlert();
        }

        public new IElementWrapperFluentApi ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.ElementAt(selector, index, tmpSelectMethod);
        }

        public new IBrowserWrapperFluentApi FireJsBlur()
        {
            return (IBrowserWrapperFluentApi)base.FireJsBlur();
        }

        public new IElementWrapperFluentApi First(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.First(selector, tmpSelectMethod);
        }

        public new IElementWrapperFluentApi FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.First(selector, tmpSelectMethod);
        }

        public new IBrowserWrapperFluentApi ForEach(string selector, Action<IElementWrapper> action, Func<string, By> tmpSelectMethod = null)
        {
            return (IBrowserWrapperFluentApi)base.ForEach(selector, action, tmpSelectMethod);
        }

        public new IBrowserWrapperFluentApi GetFrameScope(string selector)
        {
            return (IBrowserWrapperFluentApi)base.GetFrameScope(selector);
        }

        public new IElementWrapperFluentApi Last(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.Last(selector, tmpSelectMethod);
        }

        public new IElementWrapperFluentApi LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.LastOrDefault(selector, tmpSelectMethod);
        }

        public new IBrowserWrapperFluentApi SendKeys(string selector, string text, Func<string, By> tmpSelectMethod = null)
        {
            return (IBrowserWrapperFluentApi)base.SendKeys(selector, text, tmpSelectMethod);
        }

        public new IElementWrapperFluentApi Single(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.Single(selector, tmpSelectMethod);
        }

        public new IElementWrapperFluentApi SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return (IElementWrapperFluentApi)base.SingleOrDefault(selector, tmpSelectMethod);
        }

        public new IBrowserWrapperFluentApi Submit(string selector)
        {
            return (IBrowserWrapperFluentApi)base.Submit(selector);
        }

        public new IBrowserWrapperFluentApi DragAndDrop(IElementWrapper elementWrapper, IElementWrapper dropToElement, int offsetX = 0,
            int offsetY = 0)
        {
            return (IBrowserWrapperFluentApi)base.DragAndDrop(elementWrapper, dropToElement, offsetX, offsetY);
        }

        public new IBrowserWrapperFluentApi SwitchToTab(int index)
        {
            return (IBrowserWrapperFluentApi)base.SwitchToTab(index);
        }

        public new IBrowserWrapperFluentApi Wait()
        {
            return (IBrowserWrapperFluentApi)base.Wait();
        }

        public new IBrowserWrapperFluentApi Wait(int milliseconds)
        {
            return (IBrowserWrapperFluentApi)base.Wait(milliseconds);
        }

        public new IBrowserWrapperFluentApi Wait(TimeSpan interval)
        {
            return (IBrowserWrapperFluentApi)base.Wait(interval);
        }

        public new IBrowserWrapperFluentApi WaitFor(Action action, int maxTimeout, int checkInterval = 30, string failureMessage = null)
        {
            return (IBrowserWrapperFluentApi)base.WaitFor(action, maxTimeout, checkInterval, failureMessage);
        }

        public new IBrowserWrapperFluentApi WaitFor(Action checkExpression, int maxTimeout, string failureMessage,
            int checkInterval = 30)
        {
            return (IBrowserWrapperFluentApi)base.WaitFor(checkExpression, maxTimeout, failureMessage, checkInterval);
        }

        public new IBrowserWrapperFluentApi WaitFor(Func<bool> condition, int maxTimeout, string failureMessage,
            bool ignoreCertainException = true, int checkInterval = 30)
        {
            return (IBrowserWrapperFluentApi)base.WaitFor(condition, maxTimeout, failureMessage,
                ignoreCertainException);
        }

        #endregion FileUploadDialog



        public IBrowserWrapperFluentApi CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var elements = FindElements(selector);
            var validator = new HyperLinkEqualsValidator(url, kind, true, components);
            var runner = new AllOperationRunner<IElementWrapper>(elements, null);
            runner.Evaluate<UnexpectedElementStateException>(validator);

            return this;
        }


        /// <summary>
        /// Checks if browser can access given Url (browser returns status code 2??).
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlKind"></param>
        /// <returns></returns>
        public IBrowserWrapperFluentApi CheckIfUrlIsAccessible(string url, UrlKind urlKind)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserLocationException>(new UrlIsAccessibleValidator(url, urlKind));
        }


        public IBrowserWrapperFluentApi CheckIfTitleEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserException>(new TitleEqualsValidator(title,
                comparison == StringComparison.Ordinal, trim));
        }

        public IBrowserWrapperFluentApi CheckIfTitleNotEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true)
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserException>(new TitleNotEqualsValidator(title,
                comparison == StringComparison.Ordinal, trim));
        }

        public IBrowserWrapperFluentApi CheckIfTitle(Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            return (IBrowserWrapperFluentApi)EvaluateBrowserCheck<BrowserException>(new TitleValidator(expression, failureMessage));
        }

        public BrowserWrapperFluentApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }
    }
}
