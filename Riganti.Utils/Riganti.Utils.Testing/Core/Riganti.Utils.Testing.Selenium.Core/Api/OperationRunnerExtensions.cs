using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers;
using CheckIfHyperLinkEquals = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers.CheckIfHyperLinkEquals;
using CheckIfIsDisplayed = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers.CheckIfIsDisplayed;
using CheckIfIsNotDisplayed = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers.CheckIfIsNotDisplayed;
using Riganti.Utils.Testing.Selenium.Validators;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public static class OperationRunnerExtensions
    {
        public static void CheckIfInnerText(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule)
        {
            var checkIfInnerText = new CheckIfInnerText(rule);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfInnerText);
        }

        public static void CheckIfValue(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false, bool trim = true)
        {
            var checkIfValue = new CheckIfValue(value, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfValue);
        }

        public static void CheckIfInnerTextEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfInnerTextEquals = new CheckIfInnerTextEquals(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfInnerTextEquals);
        }

        public static void CheckIfText(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfText = new CheckIfText(rule, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfText);
        }

        public static void CheckIfTextEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfTextEquals = new CheckIfTextEquals(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfTextEquals);
        }
        public static void CheckIfTextNotEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfTextNotEquals = new CheckIfTextNotEquals(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfTextNotEquals);
        }

        public static void CheckIfIsDisplayed(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsDisplayed = new CheckIfIsDisplayed();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsDisplayed);
        }

        public static void CheckIfIsNotDisplayed(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsNotDisplayed = new CheckIfIsNotDisplayed();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsNotDisplayed);
        }

        public static void CheckIfIsChecked(this IOperationRunner<IElementWrapper> operationRunner)
        {
            operationRunner.CheckTagName("input", "Function CheckIfIsNotChecked() can be used on input element only.");
            operationRunner.CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var checkIfIsChecked = new CheckIfIsChecked();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsChecked);
        }

        public static void CheckIfIsNotChecked(this IOperationRunner<IElementWrapper> operationRunner)
        {
            operationRunner.CheckTagName("input", "Function CheckIfIsNotChecked() can be used on input element only.");
            operationRunner.CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var checkIfIsNotChecked = new CheckIfIsNotChecked();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsNotChecked);
        }

        public static void CheckIfIsSelected(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsSelected = new CheckIfIsSelected();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsSelected);
        }

        public static void CheckIfIsNotSelected(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsNotSelected = new CheckIfIsNotSelected();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsNotSelected);
        }

        public static void CheckIfIsEnabled(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsEnabled = new CheckIfIsEnabled();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsEnabled);
        }

        public static void CheckIfIsClickable(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsClickable = new CheckIfIsClickable();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsClickable);
        }

        public static void CheckIfIsNotClickable(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsNotClickable = new CheckIfIsNotClickable();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsNotClickable);
        }

        public static void CheckIfIsNotEnabled(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfIsNotEnabled = new CheckIfIsNotEnabled();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsNotEnabled);
        }

        public static void CheckIfContainsText(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfContainsText = new CheckIfContainsText();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfContainsText);
        }
        public static void CheckIfDoesNotContainsText(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var checkIfDoesNotContainsText = new CheckIfDoesNotContainText();
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfDoesNotContainsText);
        }

        public static void CheckIfHyperLineEquals(this IOperationRunner<IElementWrapper> operationRunner, string url, UrlKind kind, params UriComponents[] components)
        {
            var checkIfHyperLinkEquals = new CheckIfHyperLinkEquals(url, kind, components);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfHyperLinkEquals);
        }

        public static void CheckIfIsElementInView(this IOperationRunner<IElementWrapper> operationRunner, ElementWrapper element)
        {
            var checkIfIsElementInView = new CheckIfIsElementInView(element);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsElementInView);
        }

        public static void CheckIfIsElementNotInView(this IOperationRunner<IElementWrapper> operationRunner, ElementWrapper element)
        {
            var checkIfIsElementNotInView = new CheckIfIsElementNotInView(element);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsElementNotInView);
        }

        public static void CheckTagName(this IOperationRunner<IElementWrapper> operationRunner, string expectedTagName, string failureMessage = null)
        {
            operationRunner.CheckIfTagName(expectedTagName, failureMessage);
        }

        public static void CheckIfTagName(this IOperationRunner<IElementWrapper> operationRunner, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new CheckTagName(expectedTagName, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkTagName);
        }

        public static void CheckIfTagName(this IOperationRunner<IElementWrapper> operationRunner, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new CheckTagName(expectedTagNames, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkTagName);
        }

        //public static void CheckTagName(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMessage = null)
        //{
        //    operationRunner.CheckIfTagName(rule, failureMessage);
        //}

        //public static void CheckIfTagName(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMessage = null)
        //{
        //    var checkIfTagName = new CheckIfTagName(rule, failureMessage, operationRunner);
        //    operationRunner.Evaluate<UnexpectedElementStateException>(checkIfTagName);
        //}


        public static void CheckIfContainsElement(this IOperationRunner<IElementWrapper> operationRunner, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var checkIfContainsElement = new CheckIfContainsElement(cssSelector, tmpSelectMethod);
            operationRunner.Evaluate<EmptySequenceException>(checkIfContainsElement);
        }

        public static void CheckIfNotContainsElement(this IOperationRunner<IElementWrapper> operationRunner, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var checkIfNotContainsElement = new CheckIfNotContainsElement(cssSelector, tmpSelectMethod);
            operationRunner.Evaluate<MoreElementsInSequenceException>(checkIfNotContainsElement);
        }

        public static void CheckIfJsPropertyInnerText(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var checkIfJsPropertyInnerText = new CheckIfJsPropertyInnerText(rule, failureMesssage, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfJsPropertyInnerText);
        }

        public static void CheckIfJsPropertyInnerTextEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true)
        {
            var checkIfJsPropertyInnerTextEquals = new CheckIfJsPropertyInnerTextEquals(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfJsPropertyInnerTextEquals);
        }

        public static void CheckIfJsPropertyInnerHtmlEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true)
        {
            var checkIfJsPropertyInnerHtmlEquals = new CheckIfJsPropertyInnerHtmlEquals(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfJsPropertyInnerHtmlEquals);
        }

        public static void CheckIfJsPropertyInnerHtml(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfJsPropertyInnerHtml = new CheckIfJsPropertyInnerHtml(expression, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfJsPropertyInnerHtml);
        }

        public static void CheckAttribute(this IOperationRunner<IElementWrapper> operationRunner, string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttribute(attributeName, value, caseSensitive, trimValue, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkAttribute);
        }

        public static void CheckAttribute(this IOperationRunner<IElementWrapper> operationRunner, string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttribute(attributeName, allowedValues, caseSensitive, trimValue, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkAttribute);
        }

        public static void CheckAttribute(this IOperationRunner<IElementWrapper> operationRunner, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfAttribute = new CheckIfAttribute(attributeName, expression, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfAttribute);
        }

        public static void CheckClassAttribute(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false, bool trimValue = true)
        {
            operationRunner.CheckAttribute("class", value, caseSensitive, trimValue);
        }

        public static void CheckClassAttribute(this IOperationRunner<IElementWrapper> operationRunner, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            operationRunner.CheckAttribute("class", expression, failureMessage);
        }

        public static void CheckIfHasClass(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false)
        {
            operationRunner.CheckAttribute("class", p => p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void CheckIfHasNotClass(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false)
        {
            operationRunner.CheckAttribute("class", p => !p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void CheckIfHasAttribute(this IOperationRunner<IElementWrapper> operationRunner, string name)
        {
            var checkIfHasAttribute = new CheckIfHasAttribute(name);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfHasAttribute);
        }

        public static void CheckIfHasNotAttribute(this IOperationRunner<IElementWrapper> operationRunner, string name)
        {
            var checkIfHasNotAttribute = new CheckIfHasNotAttribute(name);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfHasNotAttribute);
        }
        public static void CheckIfAlertTextEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string expectedValue,
            bool caseSensitive = false, bool trim = true)
        {
            var checkIfAlertTextEquals = new CheckIfAlertTextEquals(expectedValue, caseSensitive, trim);
            operationRunner.Evaluate<AlertException>(checkIfAlertTextEquals);
        }

        public static void CheckIfAlertText(this IOperationRunner<IBrowserWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var checkIfAlertText = new CheckIfAlertText(expression, failureMessage);
            operationRunner.Evaluate<AlertException>(checkIfAlertText);
        }

        public static void CheckIfAlertTextContains(this IOperationRunner<IBrowserWrapper> operationRunner, string expectedValue, bool trim = true)
        {
            var checkIfAlertTextContains = new CheckIfAlertTextContains(expectedValue, trim);
            operationRunner.Evaluate<AlertException>(checkIfAlertTextContains);
        }

        public static void CheckUrl(this IOperationRunner<IBrowserWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfUrl = new CheckIfUrl(expression, failureMessage);
            operationRunner.Evaluate<BrowserLocationException>(checkIfUrl);
        }

        public static void CheckUrl(this IOperationRunner<IBrowserWrapper> operationRunner, string url, UrlKind urlKind, params UriComponents[] components)
        {
            var checkUrl = new CheckUrl(url, urlKind, components);
            operationRunner.Evaluate<BrowserLocationException>(checkUrl);
        }

        public static void CheckUrlEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string url)
        {
            var checkUrlExquals = new CheckUrlEquals(url);
            operationRunner.Evaluate<BrowserLocationException>(checkUrlExquals);
        }

        public static void CheckIfHyperLinkEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var checkIfHyperLinkEquals = new Validators.Checkers.BrowserWrapperCheckers.CheckIfHyperLinkEquals(selector, url, kind, components);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkIfHyperLinkEquals);
        }

        //public static void CheckIfIsDisplayed(this IOperationRunner<IBrowserWrapper> operationRunner, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        //{
        //    var checkIfIsDisplayed = new CheckIfIsDisplayed();
        //    operationRunner.Evaluate<UnexpectedElementStateException>(checkIfIsDisplayed);
        //}
        //public static void CheckIfIsNotDisplayed(this IOperationRunner<IBrowserWrapper> operationRunner, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        //{
        //    var checkIfIsNotDisplayed = new Checkers.BrowserWrapperCheckers.CheckIfIsNotDisplayed(selector, tmpSelectedMethod);
        //    operationRunner.Evaluate<BrowserException>(checkIfIsNotDisplayed);
        //}

        public static void CheckIfUrlIsAccessible(this IOperationRunner<IBrowserWrapper> operationRunner, string url, UrlKind urlKind)
        {
            var checkIfUrlIsAccessible = new CheckIfUrlIsAccessible(url, urlKind);
            operationRunner.Evaluate<BrowserException>(checkIfUrlIsAccessible);
        }

        public static void CheckIfTitleEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string title, bool caseSensitive = false, bool trim = true)
        {
            var checkIfTitleEquals = new CheckIfTitleEquals(title, caseSensitive, trim);
            operationRunner.Evaluate<BrowserException>(checkIfTitleEquals);
        }

        public static void CheckIfTitleNotEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string title, bool caseSensitive = false, bool trim = true)
        {
            var checkIfTitleNotEquals = new CheckIfTitleNotEquals(title, caseSensitive, trim);
            operationRunner.Evaluate<BrowserException>(checkIfTitleNotEquals);
        }

        public static void CheckIfTitle(this IOperationRunner<IBrowserWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var checkIfTitle = new CheckIfTitle(expression, failureMessage);
            operationRunner.Evaluate<BrowserException>(checkIfTitle);
        }


        public static void Check<TException, T>(this IOperationRunner<T> operationRunner, ICheck<T> check)
            where TException : TestExceptionBase, new()
        {
            operationRunner.Evaluate<TException>(check);
        }
    }
}
