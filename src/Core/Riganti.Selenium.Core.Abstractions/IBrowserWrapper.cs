using System;
using OpenQA.Selenium;

namespace Riganti.Selenium.Core.Abstractions
{
    public interface IBrowserWrapper : ISupportedByValidator
    {

        int ActionWaitTime { get; set; }
        string BaseUrl { get; }
        string CurrentUrl { get; }
        string CurrentUrlPath { get; }
        IWebDriver Driver { get; }
        Func<string, By> SelectMethod { get; set; }

        void ActivateScope();
        IBrowserWrapper ClearElementsContent(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper Click(string selector);
        IBrowserWrapper ConfirmAlert();
        IBrowserWrapper DismissAlert();
        void Dispose();
        IElementWrapper ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null);
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
#if net461
        IBrowserWrapper SendEnterKey();
        IBrowserWrapper SendEscKey();
        void OpenFileDialog(IElementWrapper fileInputElement, string file);
#endif
        void OpenInputFileDialog(IElementWrapper fileInputElement, string file);


        IBrowserWrapper SendKeys(string selector, string text, Func<string, By> tmpSelectMethod = null);
        void SetCssSelector();
        void SetTimeouts(TimeSpan pageLoadTimeout, TimeSpan implicitlyWait);
        IElementWrapper Single(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper Submit(string selector);
        void DropTest(string message);
        IBrowserWrapper DragAndDrop(IElementWrapper elementWrapper, IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0);
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