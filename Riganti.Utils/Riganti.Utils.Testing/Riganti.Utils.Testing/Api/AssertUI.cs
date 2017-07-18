using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Checkers;
using Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public static class AssertUI
    {
        private static readonly OperationValidator operationValidator = new OperationValidator();

        public static void CheckIfInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfInnerText = new CheckIfInnerText(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfInnerText);
        }

        public static void CheckIfInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfInnerTextEquals = new CheckIfInnerTextEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfInnerTextEquals);
        }

        public static void CheckIfText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfText = new CheckIfText(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfText);
        }

        public static void CheckIfTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfTextEquals = new CheckIfTextEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfTextEquals);
        }
        public static void CheckIfTextNotEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var checkIfTextNotEquals = new CheckIfTextNotEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfTextNotEquals);
        }

        public static void CheckIfIsDisplayed(ElementWrapper wrapper)
        {
            var checkIfIsDisplayed = new CheckIfIsDisplayed();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsDisplayed);
        }

        public static void CheckIfIsNotDisplayed(ElementWrapper wrapper)
        {
            var checkIfIsNotDisplayed = new CheckIfIsNotDisplayed();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsNotDisplayed);
        }

        public static void CheckIfIsChecked(ElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var checkIfIsChecked = new CheckIfIsChecked();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsChecked);
        }

        public static void CheckIfIsNotChecked(ElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function CheckIfIsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var checkIfIsNotChecked = new CheckIfIsNotChecked();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsNotChecked);
        }

        public static void CheckIfIsSelected(ElementWrapper wrapper)
        {
            var checkIfIsSelected = new CheckIfIsSelected();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsSelected);
        }

        public static void CheckIfIsNotSelected(ElementWrapper wrapper)
        {
            var checkIfIsNotSelected = new CheckIfIsNotSelected();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsNotSelected);
        }

        public static void CheckIfIsEnabled(ElementWrapper wrapper)
        {
            var checkIfIsEnabled = new CheckIfIsEnabled();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsEnabled);
        }

        public static void CheckIfIsClickable(ElementWrapper wrapper)
        {
            var checkIfIsClickable = new CheckIfIsClickable();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsClickable);
        }

        public static void CheckIfIsNotClickable(ElementWrapper wrapper)
        {
            var checkIfIsNotClickable = new CheckIfIsNotClickable();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsNotClickable);
        }

        public static void CheckIfIsNotEnabled(ElementWrapper wrapper)
        {
            var checkIfIsNotEnabled = new CheckIfIsNotEnabled();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsNotEnabled);
        }

        public static void CheckIfValue(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trim = true)
        {
            var checkIfValue = new CheckIfValue(value, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfValue);
        }

        public static void CheckIfContainsText(ElementWrapper wrapper)
        {
            var checkIfContainsText = new CheckIfContainsText();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfContainsText);
        }
        public static void CheckIfDoesNotContainsText(ElementWrapper wrapper)
        {
            var checkIfDoesNotContainsText = new CheckIfDoesNotContainText();
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfDoesNotContainsText);
        }

        public static void CheckIfHyperLineEquals(ElementWrapper wrapper, string url, UrlKind kind, params UriComponents[] components)
        {
            var checkIfHyperLinkEquals = new CheckIfHyperLinkEquals(url, kind, components);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfHyperLinkEquals);
        }

        public static void CheckIfIsElementInView(ElementWrapper wrapper, ElementWrapper element)
        {
            var checkIfIsElementInView = new CheckIfIsElementInView(element);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsElementInView);
        }

        public static void CheckIfIsElementNotInView(ElementWrapper wrapper, ElementWrapper element)
        {
            var checkIfIsElementNotInView = new CheckIfIsElementNotInView(element);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfIsElementNotInView);
        }

        public static void CheckTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            CheckIfTagName(wrapper, expectedTagName, failureMessage);
        }

        public static void CheckIfTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new CheckTagName(expectedTagName, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkTagName);
        }

        public static void CheckIfTagName(ElementWrapper wrapper, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new CheckTagName(expectedTagNames, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkTagName);
        }

        public static void CheckTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            CheckIfTagName(wrapper, rule, failureMessage);
        }

        public static void CheckIfTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var checkIfTagName = new CheckIfTagName(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfTagName);
        }


        public static void CheckIfContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var checkIfContainsElement = new CheckIfContainsElement(cssSelector, tmpSelectMethod);
            EvaluateCheck<EmptySequenceException, ElementWrapper>(wrapper, checkIfContainsElement);
        }

        public static void CheckIfNotContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var checkIfNotContainsElement = new CheckIfNotContainsElement(cssSelector, tmpSelectMethod);
            EvaluateCheck<MoreElementsInSequenceException, ElementWrapper>(wrapper, checkIfNotContainsElement);
        }

        public static void CheckIfJsPropertyInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var checkIfJsPropertyInnerText = new CheckIfJsPropertyInnerText(rule, failureMesssage, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfJsPropertyInnerText);
        }

        public static void CheckIfJsPropertyInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var checkIfJsPropertyInnerTextEquals = new CheckIfJsPropertyInnerTextEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfJsPropertyInnerTextEquals);
        }

        public static void CheckIfJsPropertyInnerHtmlEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var checkIfJsPropertyInnerHtmlEquals = new CheckIfJsPropertyInnerHtmlEquals(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfJsPropertyInnerHtmlEquals);
        }

        public static void CheckIfJsPropertyInnerHtml(ElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfJsPropertyInnerHtml = new CheckIfJsPropertyInnerHtml(expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfJsPropertyInnerHtml);
        }

        public static void CheckAttribute(ElementWrapper wrapper, string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttribute(attributeName, value, caseSensitive, trimValue, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkAttribute);
        }

        public static void CheckAttribute(ElementWrapper wrapper, string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttribute(attributeName, allowedValues, caseSensitive, trimValue, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkAttribute);
        }

        public static void CheckAttribute(ElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var checkIfAttribute = new CheckIfAttribute(attributeName, expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfAttribute);
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
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfHasAttribute);
        }

        public static void CheckIfHasNotAttribute(ElementWrapper wrapper, string name)
        {
            var checkIfHasNotAttribute = new CheckIfHasNotAttribute(name);
            EvaluateCheck<UnexpectedElementStateException, ElementWrapper>(wrapper, checkIfHasNotAttribute);
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
