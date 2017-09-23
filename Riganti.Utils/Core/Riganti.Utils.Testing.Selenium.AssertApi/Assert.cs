using System;
using System.Linq;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.AssertApi;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers;
using BrowserCheckers = Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers;
using ElementCheckers = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public static class Assert
    {
        private static readonly OperationValidator operationValidator = new OperationValidator();

        public static void CheckIfInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfInnerText = new CheckIfInnerText(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfInnerText);
        }

        public static void CheckIfInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfInnerTextEquals = new CheckIfInnerTextEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfInnerTextEquals);
        }

        public static void CheckIfText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfText = new CheckIfText(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfText);
        }

        public static void CheckIfTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfTextEquals = new CheckIfTextEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfTextEquals);
        }
        public static void CheckIfTextNotEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfTextNotEquals = new CheckIfTextNotEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfTextNotEquals);
        }

        public static void CheckIfIsDisplayed(ElementWrapper wrapper)
        {
            var checkIfIsDisplayed = new  ElementCheckers.CheckIfIsDisplayed();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsDisplayed);
        }

        public static void CheckIfIsNotDisplayed(ElementWrapper wrapper)
        {
            var checkIfIsNotDisplayed = new ElementCheckers.CheckIfIsNotDisplayed();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsNotDisplayed);
        }

        public static void CheckIfIsChecked(ElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var checkIfIsChecked = new CheckIfIsChecked();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsChecked);
        }

        public static void CheckIfIsNotChecked(ElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var checkIfIsNotChecked = new CheckIfIsNotChecked();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsNotChecked);
        }

        public static void CheckIfIsSelected(ElementWrapper wrapper)
        {
            var checkIfIsSelected = new CheckIfIsSelected();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsSelected);
        }

        public static void CheckIfIsNotSelected(ElementWrapper wrapper)
        {
            var checkIfIsNotSelected = new CheckIfIsNotSelected();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsNotSelected);
        }

        public static void CheckIfIsEnabled(ElementWrapper wrapper)
        {
            var checkIfIsEnabled = new CheckIfIsEnabled();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsEnabled);
        }

        public static void CheckIfIsClickable(ElementWrapper wrapper)
        {
            var checkIfIsClickable = new CheckIfIsClickable();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsClickable);
        }

        public static void CheckIfIsNotClickable(ElementWrapper wrapper)
        {
            var checkIfIsNotClickable = new CheckIfIsNotClickable();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsNotClickable);
        }

        public static void CheckIfIsNotEnabled(ElementWrapper wrapper)
        {
            var checkIfIsNotEnabled = new CheckIfIsNotEnabled();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsNotEnabled);
        }

        public static void CheckIfValue(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trim = true)
        {
            var checkIfValue = new CheckIfValue(value, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfValue);
        }

        public static void CheckIfContainsText(ElementWrapper wrapper)
        {
            var checkIfContainsText = new CheckIfContainsText();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfContainsText);
        }
        public static void CheckIfDoesNotContainsText(ElementWrapper wrapper)
        {
            var checkIfDoesNotContainsText = new CheckIfDoesNotContainText();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfDoesNotContainsText);
        }

        public static void CheckIfHyperLinkEquals(ElementWrapper wrapper, string url, UrlKind kind, params UriComponents[] components)
        {
            var checkIfHyperLinkEquals = new ElementCheckers.CheckIfHyperLinkEquals(url, kind, components);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfHyperLinkEquals);
        }

        public static void CheckIfIsElementInView(ElementWrapper wrapper, ElementWrapper element)
        {
            var checkIfIsElementInView = new CheckIfIsElementInView(element);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsElementInView);
        }

        public static void CheckIfIsElementNotInView(ElementWrapper wrapper, ElementWrapper element)
        {
            var checkIfIsElementNotInView = new CheckIfIsElementNotInView(element);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfIsElementNotInView);
        }

        public static void CheckTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            CheckIfTagName(wrapper, expectedTagName, failureMessage);
        }

        public static void CheckIfTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new CheckTagName(expectedTagName, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void CheckIfTagName(ElementWrapper wrapper, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new CheckTagName(expectedTagNames, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void CheckTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            CheckIfTagName(wrapper, rule, failureMessage);
        }

        public static void CheckIfTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfTagName = new CheckIfTagName(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfTagName);
        }


        public static void CheckIfContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var checkIfContainsElement = new CheckIfContainsElement(cssSelector, tmpSelectMethod);
            EvaluateCheck<EmptySequenceException, IElementWrapper>(wrapper, checkIfContainsElement);
        }

        public static void CheckIfNotContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var checkIfNotContainsElement = new CheckIfNotContainsElement(cssSelector, tmpSelectMethod);
            EvaluateCheck<MoreElementsInSequenceException, IElementWrapper>(wrapper, checkIfNotContainsElement);
        }

        public static void CheckIfJsPropertyInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var checkIfJsPropertyInnerText = new CheckIfJsPropertyInnerText(rule, failureMesssage, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfJsPropertyInnerText);
        }

        public static void CheckIfJsPropertyInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var checkIfJsPropertyInnerTextEquals = new CheckIfJsPropertyInnerTextEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfJsPropertyInnerTextEquals);
        }

        public static void CheckIfJsPropertyInnerHtmlEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var checkIfJsPropertyInnerHtmlEquals = new CheckIfJsPropertyInnerHtmlEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfJsPropertyInnerHtmlEquals);
        }

        public static void CheckIfJsPropertyInnerHtml(ElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfJsPropertyInnerHtml = new CheckIfJsPropertyInnerHtml(expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfJsPropertyInnerHtml);
        }

        public static void CheckAttribute(ElementWrapper wrapper, string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttribute(attributeName, value, caseSensitive, trimValue, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkAttribute);
        }

        public static void CheckAttribute(ElementWrapper wrapper, string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttribute(attributeName, allowedValues, caseSensitive, trimValue, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkAttribute);
        }

        public static void CheckAttribute(ElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfAttribute = new CheckIfAttribute(attributeName, expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfAttribute);
        }

        public static void CheckClassAttribute(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trimValue = true)
        {
            CheckAttribute(wrapper, "class", value, caseSensitive, trimValue);
        }

        public static void CheckClassAttribute(ElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            CheckAttribute(wrapper, "class", expression, failureMessage);
        }

        public static void CheckIfHasClass(ElementWrapper wrapper, string value, bool caseSensitive = false)
        {
            CheckAttribute(wrapper, "class", p => p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void CheckIfHasNotClass(ElementWrapper wrapper, string value, bool caseSensitive = false)
        {
            CheckAttribute(wrapper, "class", p => !p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void CheckIfHasAttribute(ElementWrapper wrapper, string name)
        {
            var checkIfHasAttribute = new CheckIfHasAttribute(name);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfHasAttribute);
        }

        public static void CheckIfHasNotAttribute(ElementWrapper wrapper, string name)
        {
            var checkIfHasNotAttribute = new CheckIfHasNotAttribute(name);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkIfHasNotAttribute);
        }

        public static void CheckIfAlertTextEquals(BrowserWrapper wrapper, string expectedValue,
            bool caseSensitive = false, bool trim = true)
        {
            var checkIfAlertTextEquals = new CheckIfAlertTextEquals(expectedValue, caseSensitive, trim);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, checkIfAlertTextEquals);
        }

        public static void CheckIfAlertText(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var checkIfAlertText = new CheckIfAlertText(expression, failureMessage);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, checkIfAlertText);
        }

        public static void CheckIfAlertTextContains(BrowserWrapper wrapper, string expectedValue, bool trim = true)
        {
            var checkIfAlertTextContains = new CheckIfAlertTextContains(expectedValue, trim);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, checkIfAlertTextContains);
        }

        public static void CheckUrl(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfUrl = new CheckIfUrl(expression, failureMessage);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, checkIfUrl);
        }

        public static void CheckUrl(BrowserWrapper wrapper, string url, UrlKind urlKind, params UriComponents[] components)
        {
            var checkUrl = new CheckUrl(url, urlKind, components);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, checkUrl);
        }

        public static void CheckUrlEquals(BrowserWrapper wrapper, string url)
        {
            var checkUrlExquals = new CheckUrlEquals(url);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, checkUrlExquals);
        }

        public static void CheckIfHyperLinkEquals(BrowserWrapper wrapper, string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var checkIfHyperLinkEquals = new BrowserCheckers.CheckIfHyperLinkEquals(selector, url, kind, components);
            EvaluateCheck<UnexpectedElementStateException, IBrowserWrapper>(wrapper, checkIfHyperLinkEquals);
        }

        public static void CheckIfIsDisplayed(BrowserWrapper wrapper, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        {
            var checkIfIsDisplayed = new BrowserCheckers.CheckIfIsDisplayed(selector, tmpSelectedMethod);
            EvaluateCheck<UnexpectedElementStateException, IBrowserWrapper>(wrapper, checkIfIsDisplayed);
        }
        public static void CheckIfIsNotDisplayed(BrowserWrapper wrapper, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        {
            var checkIfIsNotDisplayed = new BrowserCheckers.CheckIfIsNotDisplayed(selector, tmpSelectedMethod);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, checkIfIsNotDisplayed);
        }

        public static void CheckIfUrlIsAccessible(BrowserWrapper wrapper, string url, UrlKind urlKind)
        {
            var checkIfUrlIsAccessible = new CheckIfUrlIsAccessible(url, urlKind);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, checkIfUrlIsAccessible);
        }

        public static void CheckIfTitleEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var checkIfTitleEquals = new CheckIfTitleEquals(title, caseSensitive, trim);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, checkIfTitleEquals);
        }

        public static void CheckIfTitleNotEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var checkIfTitleNotEquals = new CheckIfTitleNotEquals(title, caseSensitive, trim);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, checkIfTitleNotEquals);
        }

        public static void CheckIfTitle(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var checkIfTitle = new CheckIfTitle(expression, failureMessage);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, checkIfTitle);
        }

        public static void Check<TException, T>(ICheck<T> check, T wrapper)
            where TException : TestExceptionBase, new()
        {
            EvaluateCheck<TException, T>(wrapper, check);
        }

        private static void EvaluateCheck<TException, T>(T wrapper, ICheck<T> check)
            where TException : TestExceptionBase, new()
        {
            var operationResult = check.Validate(wrapper);
            operationValidator.Validate<TException>(operationResult);
        }

        public static AnyOperationRunner<T> Any<T>(T[] wrappers)
        {
            return new AnyOperationRunner<T>(wrappers);
        }

        public static AllOperationRunner<T> All<T>(T[] elementWrappers)
        {
            return new AllOperationRunner<T>(elementWrappers);
        }
    }
}
