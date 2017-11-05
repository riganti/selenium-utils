using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public interface IBrowserWrapperFluentApi : IBrowserWrapper
    {

        IBrowserWrapperFluentApi CheckIfAlertText(Expression<Func<string, bool>> expression, string failureMessage = "");
        IBrowserWrapperFluentApi CheckIfAlertTextContains(string expectedValue, bool trim = true);
        IBrowserWrapperFluentApi CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true);
        IBrowserWrapperFluentApi CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components);
        IElementWrapperCollection CheckIfIsDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapperCollection CheckIfIsNotDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapperFluentApi CheckIfTitle(Expression<Func<string, bool>> expression, string failureMessage = "");
        IBrowserWrapperFluentApi CheckIfTitleEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true);
        IBrowserWrapperFluentApi CheckIfTitleNotEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true);
        IBrowserWrapperFluentApi CheckIfUrlIsAccessible(string url, UrlKind urlKind);
        IBrowserWrapperFluentApi CheckUrl(Expression<Func<string, bool>> expression, string failureMessage = null);
        IBrowserWrapperFluentApi CheckUrl(string url, UrlKind urlKind, params UriComponents[] components);
        IBrowserWrapperFluentApi CheckUrlEquals(string url);

        bool CompareUrl(string url);
        bool CompareUrl(string url, UrlKind urlKind, params UriComponents[] components);
        IBrowserWrapperFluentApi FileUploadDialogSelect(IElementWrapper fileUploadOpener, string fullFileName);




        new IBrowserWrapperFluentApi ClearElementsContent(string selector, Func<string, By> tmpSelectMethod = null);
        new IBrowserWrapperFluentApi Click(string selector);
        new IBrowserWrapperFluentApi ConfirmAlert();
        new IBrowserWrapperFluentApi DismissAlert();
        new IElementWrapperFluentApi ElementAt(string selector, int index, Func<string, By> tmpSelectMethod = null);
        new IBrowserWrapperFluentApi FireJsBlur();
        new IElementWrapperFluentApi First(string selector, Func<string, By> tmpSelectMethod = null);
        new IElementWrapperFluentApi FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        new IBrowserWrapperFluentApi ForEach(string selector, Action<IElementWrapper> action, Func<string, By> tmpSelectMethod = null);
        new IBrowserWrapperFluentApi GetFrameScope(string selector);
        new IElementWrapperFluentApi Last(string selector, Func<string, By> tmpSelectMethod = null);
        new IElementWrapperFluentApi LastOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
#if net461
        new IBrowserWrapperFluentApi  SendEnterKey();
        new IBrowserWrapperFluentApi  SendEscKey();
        void OpenFileDialog(IElementWrapper fileInputElement, string file);
#endif


        new IBrowserWrapperFluentApi SendKeys(string selector, string text, Func<string, By> tmpSelectMethod = null);
        new IElementWrapperFluentApi Single(string selector, Func<string, By> tmpSelectMethod = null);
        new IElementWrapperFluentApi SingleOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        new IBrowserWrapperFluentApi Submit(string selector);
        new IBrowserWrapperFluentApi DragAndDrop(IElementWrapper elementWrapper, IElementWrapper dropToElement, int offsetX = 0, int offsetY = 0);
        new IBrowserWrapperFluentApi SwitchToTab(int index);
        new IBrowserWrapperFluentApi Wait();
        new IBrowserWrapperFluentApi Wait(int milliseconds);
        new IBrowserWrapperFluentApi Wait(TimeSpan interval);
        new IBrowserWrapperFluentApi WaitFor(Action action, int maxTimeout, int checkInterval = 500, string failureMessage = null);
        new IBrowserWrapperFluentApi WaitFor(Action checkExpression, int maxTimeout, string failureMessage, int checkInterval = 500);
        new IBrowserWrapperFluentApi WaitFor(Func<bool> condition, int maxTimeout, string failureMessage, bool ignoreCertainException = true, int checkInterval = 500);
    }
}