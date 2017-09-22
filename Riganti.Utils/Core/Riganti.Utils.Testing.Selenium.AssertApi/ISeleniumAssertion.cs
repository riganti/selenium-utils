using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.AssertApi
{
    public interface ISeleniumAssertion
    {
        AllOperationRunner<T> All<T>(T[] elementWrappers);
        AnyOperationRunner<T> Any<T>(T[] wrappers);
        void Check<TException, T>(ICheck<T> check, T wrapper) where TException : TestExceptionBase, new();
        void CheckAttribute(ElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = null);
        void CheckAttribute(ElementWrapper wrapper, string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null);
        void CheckAttribute(ElementWrapper wrapper, string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null);
        void CheckClassAttribute(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trimValue = true);
        void CheckClassAttribute(ElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = "");
        void CheckIfAlertText(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "");
        void CheckIfAlertTextContains(BrowserWrapper wrapper, string expectedValue, bool trim = true);
        void CheckIfAlertTextEquals(BrowserWrapper wrapper, string expectedValue, bool caseSensitive = false, bool trim = true);
        void CheckIfContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null);
        void CheckIfContainsText(ElementWrapper wrapper);
        void CheckIfDoesNotContainsText(ElementWrapper wrapper);
        void CheckIfHasAttribute(ElementWrapper wrapper, string name);
        void CheckIfHasClass(ElementWrapper wrapper, string value, bool caseSensitive = false);
        void CheckIfHasNotAttribute(ElementWrapper wrapper, string name);
        void CheckIfHasNotClass(ElementWrapper wrapper, string value, bool caseSensitive = false);
        void CheckIfHyperLinkEquals(BrowserWrapper wrapper, string selector, string url, UrlKind kind, params UriComponents[] components);
        void CheckIfHyperLinkEquals(ElementWrapper wrapper, string url, UrlKind kind, params UriComponents[] components);
        void CheckIfInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null);
        void CheckIfInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null);
        void CheckIfIsChecked(ElementWrapper wrapper);
        void CheckIfIsClickable(ElementWrapper wrapper);
        void CheckIfIsDisplayed(BrowserWrapper wrapper, string selector, Expression<Func<string, By>> tmpSelectedMethod = null);
        void CheckIfIsDisplayed(ElementWrapper wrapper);
        void CheckIfIsElementInView(ElementWrapper wrapper, ElementWrapper element);
        void CheckIfIsElementNotInView(ElementWrapper wrapper, ElementWrapper element);
        void CheckIfIsEnabled(ElementWrapper wrapper);
        void CheckIfIsNotChecked(ElementWrapper wrapper);
        void CheckIfIsNotClickable(ElementWrapper wrapper);
        void CheckIfIsNotDisplayed(BrowserWrapper wrapper, string selector, Expression<Func<string, By>> tmpSelectedMethod = null);
        void CheckIfIsNotDisplayed(ElementWrapper wrapper);
        void CheckIfIsNotEnabled(ElementWrapper wrapper);
        void CheckIfIsNotSelected(ElementWrapper wrapper);
        void CheckIfIsSelected(ElementWrapper wrapper);
        void CheckIfJsPropertyInnerHtml(ElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null);
        void CheckIfJsPropertyInnerHtmlEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true);
        void CheckIfJsPropertyInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true);
        void CheckIfJsPropertyInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true);
        void CheckIfNotContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null);
        void CheckIfTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null);
        void CheckIfTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null);
        void CheckIfTagName(ElementWrapper wrapper, string[] expectedTagNames, string failureMessage = null);
        void CheckIfText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null);
        void CheckIfTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null);
        void CheckIfTextNotEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null);
        void CheckIfTitle(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "");
        void CheckIfTitleEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true);
        void CheckIfTitleNotEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true);
        void CheckIfUrlIsAccessible(BrowserWrapper wrapper, string url, UrlKind urlKind);
        void CheckIfValue(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trim = true);
        void CheckTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null);
        void CheckTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null);
        void CheckUrl(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null);
        void CheckUrl(BrowserWrapper wrapper, string url, UrlKind urlKind, params UriComponents[] components);
        void CheckUrlEquals(BrowserWrapper wrapper, string url);
    }
}