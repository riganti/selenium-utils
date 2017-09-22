using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface IBrowserWrapperPseudoFluentApi
    {
        int ActionWaitTime { get; set; }
        string BaseUrl { get; }
        string CurrentUrl { get; }
        string CurrentUrlPath { get; }
        IWebDriver Driver { get; }
        Func<string, By> SelectMethod { get; set; }

        void ActivateScope();
        IBrowserWrapper CheckIfAlertText(Func<string, bool> expression, string failureMessage = "");
        IBrowserWrapper CheckIfAlertTextContains(string expectedValue, bool trim = true);
        IBrowserWrapper CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true);
        IBrowserWrapper CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components);
        IElementWrapperCollection CheckIfIsDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapperCollection CheckIfIsNotDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper CheckIfTitle(Func<string, bool> func, string failureMessage = "");
        IBrowserWrapper CheckIfTitleEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true);
        IBrowserWrapper CheckIfTitleNotEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true);
        IBrowserWrapper CheckIfUrlIsAccessible(string url, UrlKind urlKind);
        IBrowserWrapper CheckUrl(Func<string, bool> expression, string failureMessage = null);
        IBrowserWrapper CheckUrl(string url, UrlKind urlKind, params UriComponents[] components);
        IBrowserWrapper CheckUrlEquals(string url);
        IBrowserWrapper ClearElementsContent(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper Click(string selector);
        bool CompareUrl(string url);
        bool CompareUrl(string url, UrlKind urlKind, params UriComponents[] components);
        IBrowserWrapper ConfirmAlert();
        IBrowserWrapper DismissAlert();
        void Dispose();
        IBrowserWrapper DragAndDrop(IElementWrapper dragOnElement, IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0);
        void DropTest(string message);
        IElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper FileUploadDialogSelect(IElementWrapper fileUploadOpener, string fullFileName);
        IElementWrapperCollection FindElements(By selector);
        IElementWrapperCollection FindElements(string cssSelector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper FireJsBlur();
        IElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper ForEach(string selector, Action<IElementWrapper> action, Func<string, By> tmpSelectMethod = null);
        string GetAbsoluteUrl(string relativeUrl);
        IAlert GetAlert();
        string GetAlertText();
        IBrowserWrapper GetFrameScope(string selector);
        IJavaScriptExecutor GetJavaScriptExecutor();
        string GetTitle();
        bool HasAlert();
        bool IsDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper Last(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        void LogError(string message, Exception ex);
        void LogInfo(string message);
        void LogVerbose(string message);
        void NavigateBack();
        void NavigateForward();
        void NavigateToUrl();
        void NavigateToUrl(string url);
        void Refresh();
        IBrowserWrapper SendEnterKey();
        IBrowserWrapper SendEscKey();
        IBrowserWrapper SendKeys(string selector, string text, Func<string, By> tmpSelectMethod = null);
        void SetCssSelector();
        void SetTimeouts(TimeSpan pageLoadTimeout, TimeSpan implicitlyWait);
        IElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper Submit(string selector);
        IBrowserWrapper SwitchToTab(int index);
        void TakeScreenshot(string filename, ScreenshotImageFormat? format = null);
        T ThrowIfIsNull<T>(T obj, string message);
        IBrowserWrapper Wait();
        IBrowserWrapper Wait(int milliseconds);
        IBrowserWrapper Wait(TimeSpan interval);
        IBrowserWrapper WaitFor(Action action, int maxTimeout, int checkInterval = 500, string failureMessage = null);
        IBrowserWrapper WaitFor(Action checkExpression, int maxTimeout, string failureMessage, int checkInterval = 500);
        IBrowserWrapper WaitFor(Func<bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true, int checkInterval = 500);
        IWebDriver _GetInternalWebDriver();
    }
}