using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class BrowserWrapperPseudoFluentApi : BrowserWrapper, IBrowserWrapper, IBrowserWrapperPseudoFluentApi
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
            var currentUri = new Uri(CurrentUrl);
            //support relative domain
            //(new Uri() cannot parse the url correctly when the host is missing
            if (urlKind == UrlKind.Relative)
            {
                url = url.StartsWith("/") ? $"{currentUri.Scheme}://{currentUri.Host}{url}" : $"{currentUri.Scheme}://{currentUri.Host}/{url}";
            }

            if (urlKind == UrlKind.Absolute && url.StartsWith("//"))
            {
                if (!string.IsNullOrWhiteSpace(currentUri.Scheme))
                {
                    url = currentUri.Scheme + ":" + url;
                }
            }

            var expectedUri = new Uri(url, UriKind.Absolute);

            if (components.Length == 0)
            {
                throw new BrowserLocationException($"Function CheckUrlCheckUrl(string, UriKind, params UriComponents) has to have one UriComponents at least.");
            }
            UriComponents finalComponent = components[0];
            components.ToList().ForEach(s => finalComponent |= s);

            return Uri.Compare(currentUri, expectedUri, finalComponent, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Clicks on element.
        /// </summary>
        public IBrowserWrapper Click(string selector)
        {
            First(selector).Click();
            Wait();
            return this;
        }

        /// <summary>
        /// Submits this element to the web server.
        /// </summary>
        /// <remarks>
        /// If this current element is a form, or an element within a form,
        ///             then this will be submitted to the web server. If this causes the current
        ///             page to change, then this method will block until the new page is loaded.
        /// </remarks>
        public IBrowserWrapper Submit(string selector)
        {
            First(selector).Submit();
            Wait();
            return this;
        }

    
        public string GetAlertText()
        {
            var alert = GetAlert();
            return alert?.Text;
        }

        public IBrowserWrapper CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            return EvaluateElementCheck<AlertException>(
                new AlertTextEqualsValidator(expectedValue, caseSensitive, trim));
        }

        public bool HasAlert()
        {
            try
            {
                GetAlert();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IAlert GetAlert()
        {
            IAlert alert;
            try

            {
                alert = Driver.SwitchTo().Alert();
            }
            catch (Exception ex)
            {
                throw new AlertException("Alert not visible.", ex);
            }
            if (alert == null)
                throw new AlertException("Alert not visible.");
            return alert;
        }

        /// <summary>
        /// Checks if modal dialog (Alert) contains specified text as a part of provided text from the dialog.
        /// </summary>
        public IBrowserWrapper CheckIfAlertTextContains(string expectedValue, bool trim = true)
        {
            return EvaluateElementCheck<AlertException>(new AlertTextContainsValidator(expectedValue, trim));
            
        }

        /// <summary>
        /// Checks if modal dialog (Alert) text equals with specified text.
        /// </summary>
        public IBrowserWrapper CheckIfAlertText(Func<string, bool> expression, string failureMessage = "")
        {
            return EvaluateElementCheck<AlertException>(new AlertTextValidator(expression == null
                ? default(Expression<Func<string, bool>>)
                : (s => expression(s)))); //TODO change method parametr Expression<Func<string, bool>>
        }

        /// <summary>
        /// Confirms modal dialog (Alert).
        /// </summary>
        public IBrowserWrapper ConfirmAlert()
        {
            Driver.SwitchTo().Alert().Accept();
            Wait();
            return this;
        }

        /// <summary>
        /// Dismisses modal dialog (Alert).
        /// </summary>
        public IBrowserWrapper DismissAlert()
        {
            Driver.SwitchTo().Alert().Dismiss();
            Wait();
            return this;
        }



        /// <param name="tmpSelectMethod">temporary method which determine how the elements are selected</param>

        public IElementWrapperCollection CheckIfIsDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var collection = FindElements(selector, tmpSelectMethod);
            var result = collection.ThrowIfSequenceEmpty().All(s => s.IsDisplayed());
            if (!result)
            {
                var index = collection.IndexOf(collection.First(s => !s.IsDisplayed()));
                throw new UnexpectedElementStateException($"One or more elements are not displayed. Selector '{selector}', Index of non-displayed element: {index}");
            }
            return collection;
        }

        ///<summary>Provides elements that satisfies the selector condition at specific position.</summary>
        /// <param name="tmpSelectMethod">temporary method which determine how the elements are selected</param>
        public IElementWrapperCollection CheckIfIsNotDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var collection = FindElements(selector, tmpSelectMethod);
            var result = collection.All(s => s.IsDisplayed()) && collection.Any();
            if (result)
            {
                var index = collection.Any() ? collection.IndexOf(collection.First(s => !s.IsDisplayed())) : -1;
                throw new UnexpectedElementStateException($"One or more elements are displayed and they shouldn't be. Selector '{selector}', Index of non-displayed element: {index}");
            }
            return collection;
        }

        public IBrowserWrapper FireJsBlur()
        {
            GetJavaScriptExecutor()?.ExecuteScript("if(document.activeElement && document.activeElement.blur) {document.activeElement.blur()}");
            return this;
        }

    
        /// <param name="tmpSelectMethod">temporary method which determine how the elements are selected</param>

        public IBrowserWrapper SendKeys(string selector, string text, Func<string, By> tmpSelectMethod = null)
        {
            FindElements(selector, tmpSelectMethod).ForEach(s => { s.SendKeys(text); s.Wait(); });
            return this;
        }

        /// <summary>
        /// Removes content from selected elements
        /// </summary>
        /// <param name="tmpSelectMethod">temporary method which determine how the elements are selected</param>
        public IBrowserWrapper ClearElementsContent(string selector, Func<string, By> tmpSelectMethod = null)
        {
            FindElements(selector, tmpSelectMethod).ForEach(s => { s.Clear(); s.Wait(); });
            return this;
        }

        #region CheckUrl

        /// <summary>
        /// Checks exact match with CurrentUrl
        /// </summary>
        /// <param name="url">This url is compared with CurrentUrl.</param>
        public IBrowserWrapper CheckUrlEquals(string url)
        {
            return EvaluateElementCheck<BrowserLocationException>(new CheckUrlEquals(url));
        }

        /// <summary>
        /// Checks if CurrentUrl satisfies the condition defined by lamda expression
        /// </summary>
        /// <param name="expression">The condition</param>
        /// <param name="failureMessage">Failure message</param>
        public IBrowserWrapper CheckUrl(Func<string, bool> expression, string failureMessage = null)
        {
            if (!expression(CurrentUrl))
            {
                throw new BrowserLocationException($"Current url is not expected. Current url: '{CurrentUrl}'. " + (failureMessage ?? ""));
            }
            return this;
        }

        /// <summary>
        /// Checks url by its parts
        /// </summary>
        /// <param name="url">This url is compared with CurrentUrl.</param>
        /// <param name="urlKind">Determine whether url parameter contains relative or absolute path.</param>
        /// <param name="components">Determine what parts of urls are compared.</param>
        public IBrowserWrapper CheckUrl(string url, UrlKind urlKind, params UriComponents[] components)
        {
            return EvaluateElementCheck<BrowserLocationException>(new CheckUrl(url, urlKind, components));
        }

        #endregion CheckUrl

        #region FileUploadDialog

        /// <summary>
        /// Opens file dialog and sends keys with full path to file, that should be uploaded.
        /// </summary>
        /// <param name="fileUploadOpener">Element that opens file dialog after it is clicked.</param>
        /// <param name="fullFileName">Full path to file that is intended to be uploaded.</param>
        public virtual IBrowserWrapper FileUploadDialogSelect(IElementWrapper fileUploadOpener, string fullFileName)
        {
            if (fileUploadOpener.GetTagName() == "input" && fileUploadOpener.HasAttribute("type") && fileUploadOpener.GetAttribute("type") == "file")
            {
                fileUploadOpener.SendKeys(fullFileName);
                Wait();
            }
            else
            {
                // open file dialog
                fileUploadOpener.Click();
                Wait();
                //Another wait is needed because without it sometimes few chars from file path are skipped.
                Wait(1000);
                // write the full path to the dialog
                throw new NotImplementedException();
                //       System.Windows.Forms.SendKeys.SendWait(fullFileName);
                Wait();
                SendEnterKey();
            }
            return this;
        }

        public virtual IBrowserWrapper SendEnterKey()
        {

            throw new NotImplementedException();
            //System.Windows.Forms.SendKeys.SendWait("{Enter}");
            Wait();
            return this;
        }

        public virtual IBrowserWrapper SendEscKey()
        {
            throw new NotImplementedException();

            //System.Windows.Forms.SendKeys.SendWait("{ESC}");
            Wait();
            return this;
        }

        #endregion FileUploadDialog

    

        public IBrowserWrapper CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            ForEach(selector, element =>
            {
                element.CheckIfHyperLinkEquals(url, kind, components);
            });
            return this;
        }

     
        /// <summary>
        /// Checks if browser can access given Url (browser returns status code 2??).
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlKind"></param>
        /// <returns></returns>
        public IBrowserWrapper CheckIfUrlIsAccessible(string url, UrlKind urlKind)
        {
            return EvaluateElementCheck<BrowserException>(new UrlIsAccessibleValidator(url, urlKind));
        }

        public string GetTitle() => Driver.Title;

        public IBrowserWrapper CheckIfTitleEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true)
        {
            return EvaluateElementCheck<BrowserException>(new TitleEqualsValidator(title,
                comparison == StringComparison.Ordinal, trim));
        }

        public IBrowserWrapper CheckIfTitleNotEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true)
        {
            return EvaluateElementCheck<BrowserException>(new TitleNotEqualsValidator(title,
                comparison == StringComparison.Ordinal, trim));
        }

        public IBrowserWrapper CheckIfTitle(Func<string, bool> func, string failureMessage = "")
        {
            return EvaluateElementCheck<BrowserException>(
                new TitleValidator(func == null ? default(Expression<Func<string, bool>>) : (s => func(s)),
                    failureMessage));  //TODO change method parametr Expression<Func<string, bool>>
        }

        /// <summary>
        /// Drag on element dragOnElement and drop to dropToElement with offsetX and offsetY.
        /// </summary>
        /// <param name="dragOnElement"></param>
        /// <param name="dropToElement"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public IBrowserWrapper DragAndDrop(IElementWrapper dragOnElement, IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0)
        {
            var builder = new Actions(_GetInternalWebDriver());
            var from = dragOnElement.WebElement;
            var to = dropToElement.WebElement;
            var dragAndDrop = builder.ClickAndHold(from).MoveToElement(to, offsetX, offsetY).Release(to).Build();
            dragAndDrop.Perform();
            return this;
        }

        public BrowserWrapperPseudoFluentApi(IWebBrowser browser, IWebDriver driver, ITestInstance testInstance, ScopeOptions scope) : base(browser, driver, testInstance, scope)
        {
        }
    }
}
