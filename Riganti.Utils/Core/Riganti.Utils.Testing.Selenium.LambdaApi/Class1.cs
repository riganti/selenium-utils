using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.LambdaApi
{
    public static class SeleniumTestExecutorExtensions
    {
        /// <summary>
        /// Runs the specified testBody in all configured browsers.
        /// </summary>
        public static void RunInAllBrowsers(this ISeleniumTest executor, Action<BrowserWrapperLambdaApi> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            executor.TestSuiteRunner.RunInAllBrowsers(executor, (Action<IBrowserWrapper>)testBody, callerMemberName, callerFilePath, callerLineNumber);
        }

    }

    public class BrowserWrapperLambdaApi : IBrowserWrapper
    {
        public int ActionWaitTime { get; set; }
        public string BaseUrl { get; }
        public string CurrentUrl { get; }
        public string CurrentUrlPath { get; }
        public IWebDriver Driver { get; }
        public Func<string, By> SelectMethod { get; set; }
        public void ActivateScope()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfAlertText(Func<string, bool> expression, string failureMessage = "")
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfAlertTextContains(string expectedValue, bool trim = true)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            throw new NotImplementedException();
        }

        public IElementWrapperCollection CheckIfIsDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IElementWrapperCollection CheckIfIsNotDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfTitle(Func<string, bool> func, string failureMessage = "")
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfTitleEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase,
            bool trim = true)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfTitleNotEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase,
            bool trim = true)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckIfUrlIsAccessible(string url, UrlKind urlKind)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckUrl(Func<string, bool> expression, string failureMessage = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckUrl(string url, UrlKind urlKind, params UriComponents[] components)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper CheckUrlEquals(string url)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper ClearElementsContent(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper Click(string selector)
        {
            throw new NotImplementedException();
        }

        public bool CompareUrl(string url)
        {
            throw new NotImplementedException();
        }

        public bool CompareUrl(string url, UrlKind urlKind, params UriComponents[] components)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper ConfirmAlert()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper DismissAlert()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper DragAndDrop(IElementWrapper dragOnElement, IElementWrapper dropToElement, int offsetX = 0,
            int offsetY = 0)
        {
            throw new NotImplementedException();
        }

        public void DropTest(string message)
        {
            throw new NotImplementedException();
        }

        public IElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper FileUploadDialogSelect(IElementWrapper fileUploadOpener, string fullFileName)
        {
            throw new NotImplementedException();
        }

        public IElementWrapperCollection FindElements(By selector)
        {
            throw new NotImplementedException();
        }

        public IElementWrapperCollection FindElements(string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper FireJsBlur()
        {
            throw new NotImplementedException();
        }

        public IElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper ForEach(string selector, Action<IElementWrapper> action, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public string GetAbsoluteUrl(string relativeUrl)
        {
            throw new NotImplementedException();
        }

        public IAlert GetAlert()
        {
            throw new NotImplementedException();
        }

        public string GetAlertText()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper GetFrameScope(string selector)
        {
            throw new NotImplementedException();
        }

        public IJavaScriptExecutor GetJavaScriptExecutor()
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
        {
            throw new NotImplementedException();
        }

        public bool HasAlert()
        {
            throw new NotImplementedException();
        }

        public bool IsDisplayed(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void LogVerbose(string message)
        {
            throw new NotImplementedException();
        }

        public void NavigateBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateForward()
        {
            throw new NotImplementedException();
        }

        public void NavigateToUrl()
        {
            throw new NotImplementedException();
        }

        public void NavigateToUrl(string url)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper SendEnterKey()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper SendEscKey()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper SendKeys(string selector, string text, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public void SetCssSelector()
        {
            throw new NotImplementedException();
        }

        public void SetTimeouts(TimeSpan pageLoadTimeout, TimeSpan implicitlyWait)
        {
            throw new NotImplementedException();
        }

        public IElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper Submit(string selector)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper SwitchToTab(int index)
        {
            throw new NotImplementedException();
        }

        public void TakeScreenshot(string filename, ScreenshotImageFormat? format = null)
        {
            throw new NotImplementedException();
        }

        public T ThrowIfIsNull<T>(T obj, string message)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper Wait()
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper Wait(int milliseconds)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper Wait(TimeSpan interval)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper WaitFor(Action action, int maxTimeout, int checkInterval = 500, string failureMessage = null)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper WaitFor(Action checkExpression, int maxTimeout, string failureMessage, int checkInterval = 500)
        {
            throw new NotImplementedException();
        }

        public IBrowserWrapper WaitFor(Func<bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true,
            int checkInterval = 500)
        {
            throw new NotImplementedException();
        }

        public IWebDriver _GetInternalWebDriver()
        {
            throw new NotImplementedException();
        }
    }
}
