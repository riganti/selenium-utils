using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public interface IBrowserWrapperFluentApi : IBrowserWrapper
    {
    
        IBrowserWrapper CheckIfAlertText(Expression<Func<string, bool>> expression, string failureMessage = "");
        IBrowserWrapper CheckIfAlertTextContains(string expectedValue, bool trim = true);
        IBrowserWrapper CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true);
        IBrowserWrapper CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components);
        IElementWrapperCollection CheckIfIsDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapperCollection CheckIfIsNotDisplayed(string selector, Func<string, By> tmpSelectMethod = null);
        IBrowserWrapper CheckIfTitle(Expression<Func<string, bool>> expression, string failureMessage = "");
        IBrowserWrapper CheckIfTitleEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true);
        IBrowserWrapper CheckIfTitleNotEquals(string title, StringComparison comparison = StringComparison.OrdinalIgnoreCase, bool trim = true);
        IBrowserWrapper CheckIfUrlIsAccessible(string url, UrlKind urlKind);
        IBrowserWrapper CheckUrl(Expression<Func<string, bool>> expression, string failureMessage = null);
        IBrowserWrapper CheckUrl(string url, UrlKind urlKind, params UriComponents[] components);
        IBrowserWrapper CheckUrlEquals(string url);
      
        bool CompareUrl(string url);
        bool CompareUrl(string url, UrlKind urlKind, params UriComponents[] components);
        IBrowserWrapper FileUploadDialogSelect(IElementWrapper fileUploadOpener, string fullFileName);
    }
}