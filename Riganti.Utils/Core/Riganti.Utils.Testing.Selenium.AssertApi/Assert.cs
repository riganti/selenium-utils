using System;
using System.Collections;
using System.Collections.Generic;
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
        private static readonly OperationValidator OperationValidator = new OperationValidator();

        public static void InnerText(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var innerText = new InnerTextValidator(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, innerText);
        }

        public static void InnerTextEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var innerTextEquals = new InnerTextEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, innerTextEquals);
        }

        public static void Text(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var text = new TextValidator(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, text);
        }

        public static void TextEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var textEquals = new TextEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, textEquals);
        }
        public static void TextNotEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var textNotEquals = new TextNotEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, textNotEquals);
        }

        public static void IsDisplayed(IElementWrapper wrapper)
        {
            var isDisplayed = new  ElementCheckers.IsDisplayedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isDisplayed);
        }

        public static void IsNotDisplayed(IElementWrapper wrapper)
        {
            var isNotDisplayed = new ElementCheckers.IsNotDisplayedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotDisplayed);
        }

        public static void IsChecked(IElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function IsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var isChecked = new IsCheckedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isChecked);
        }

        public static void IsNotChecked(IElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function IsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var isNotChecked = new IsNotCheckedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotChecked);
        }

        public static void IsSelected(IElementWrapper wrapper)
        {
            var isSelected = new IsSelectedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isSelected);
        }

        public static void IsNotSelected(IElementWrapper wrapper)
        {
            var isNotSelected = new IsNotSelectedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotSelected);
        }

        public static void IsEnabled(IElementWrapper wrapper)
        {
            var isEnabled = new IsEnabledValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isEnabled);
        }

        public static void IsClickable(IElementWrapper wrapper)
        {
            var isClickable = new IsClickableValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isClickable);
        }

        public static void IsNotClickable(IElementWrapper wrapper)
        {
            var isNotClickable = new IsNotClickableValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotClickable);
        }

        public static void IsNotEnabled(IElementWrapper wrapper)
        {
            var isNotEnabled = new IsNotEnabledValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotEnabled);
        }

        public static void Value(IElementWrapper wrapper, string value, bool caseSensitive = false, bool trim = true)
        {
            var Value = new ValueValidator(value, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, Value);
        }

        public static void ContainsText(IElementWrapper wrapper)
        {
            var containsText = new ContainsTextValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, containsText);
        }
        public static void DoesNotContainsText(IElementWrapper wrapper)
        {
            var doesNotContainsText = new DoesNotContainTextValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, doesNotContainsText);
        }

        public static void HyperLinkEquals(IElementWrapper wrapper, string url, UrlKind kind, params UriComponents[] components)
        {
            var hyperLinkEquals = new HyperLinkEqualsValidator(url, kind, components);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, hyperLinkEquals);
        }

        public static void IsElementInView(IElementWrapper wrapper, ElementWrapper element)
        {
            var isElementInView = new IsElementInViewValidator(element);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isElementInView);
        }

        public static void IsElementNotInView(IElementWrapper wrapper, ElementWrapper element)
        {
            var isElementNotInView = new IsElementNotInViewValidator(element);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, isElementNotInView);
        }

        public static void CheckTagName(IElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            TagName(wrapper, expectedTagName, failureMessage);
        }

        public static void TagName(IElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagName, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void TagName(IElementWrapper wrapper, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagNames, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void CheckTagName(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            TagName(wrapper, rule, failureMessage);
        }

        public static void TagName(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var tagName = new TagNameValidator(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, tagName);
        }


        public static void ContainsElement(IElementWrapper wrapper, string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            var containsElement = new ContainsElementValidator(cssSelector, tmpSelectMethod);
            EvaluateCheck<EmptySequenceException, IElementWrapper>(wrapper, containsElement);
        }

        public static void NotContainsElement(IElementWrapper wrapper, string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            var notContainsElement = new NotContainsElementValidator(cssSelector, tmpSelectMethod);
            EvaluateCheck<MoreElementsInSequenceException, IElementWrapper>(wrapper, notContainsElement);
        }

        public static void JsPropertyInnerText(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var jsPropertyInnerText = new JsPropertyInnerTextValidator(rule, failureMesssage, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerText);
        }

        public static void JsPropertyInnerTextEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var jsPropertyInnerTextEquals = new JsPropertyInnerTextEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerTextEquals);
        }

        public static void JsPropertyInnerHtmlEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var jsPropertyInnerHtmlEquals = new JsPropertyInnerHtmlEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerHtmlEquals);
        }

        public static void JsPropertyInnerHtml(IElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var jsPropertyInnerHtml = new JsPropertyInnerHtmlValidator(expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerHtml);
        }

        public static void CheckAttribute(IElementWrapper wrapper, string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttributeValidator(attributeName, value, caseSensitive, trimValue, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkAttribute);
        }

        public static void CheckAttribute(IElementWrapper wrapper, string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new CheckAttributeValidator(attributeName, allowedValues, caseSensitive, trimValue, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkAttribute);
        }

        public static void CheckAttribute(IElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var attribute = new AttributeValidator(attributeName, expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, attribute);
        }

        public static void CheckClassAttribute(IElementWrapper wrapper, string value, bool caseSensitive = false, bool trimValue = true)
        {
            CheckAttribute(wrapper, "class", value, caseSensitive, trimValue);
        }

        public static void CheckClassAttribute(IElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            CheckAttribute(wrapper, "class", expression, failureMessage);
        }

        public static void HasClass(IElementWrapper wrapper, string value, bool caseSensitive = false)
        {
            CheckAttribute(wrapper, "class", p => p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasNotClass(IElementWrapper wrapper, string value, bool caseSensitive = false)
        {
            CheckAttribute(wrapper, "class", p => !p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasAttribute(IElementWrapper wrapper, string name)
        {
            var hasAttribute = new HasAttributeValidator(name);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, hasAttribute);
        }

        public static void HasNotAttribute(IElementWrapper wrapper, string name)
        {
            var hasNotAttribute = new HasNotAttributeValidator(name);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, hasNotAttribute);
        }

        public static void AlertTextEquals(BrowserWrapper wrapper, string expectedValue,
            bool caseSensitive = false, bool trim = true)
        {
            var alertTextEquals = new AlertTextEqualsValidator(expectedValue, caseSensitive, trim);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, alertTextEquals);
        }

        public static void AlertText(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var alertText = new AlertTextValidator(expression, failureMessage);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, alertText);
        }

        public static void AlertTextContains(BrowserWrapper wrapper, string expectedValue, bool trim = true)
        {
            var alertTextContains = new AlertTextContainsValidator(expectedValue, trim);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, alertTextContains);
        }

        public static void CheckUrl(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var url = new CurrentUrlValidator(expression, failureMessage);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, url);
        }

        public static void CheckUrl(BrowserWrapper wrapper, string url, UrlKind urlKind, params UriComponents[] components)
        {
            var checkUrl = new UrlValidator(url, urlKind, components);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, checkUrl);
        }

        public static void CheckUrlEquals(BrowserWrapper wrapper, string url)
        {
            var checkUrlExquals = new UrlEqualsValidator(url);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, checkUrlExquals);
        }

        public static void HyperLinkEquals(BrowserWrapper wrapper, string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var elements = wrapper.FindElements(selector);
            var operationRunner = All(elements);
            var hyperLinkEquals = new HyperLinkEqualsValidator(url, kind, components);
            operationRunner.Evaluate<UnexpectedElementStateException>(hyperLinkEquals);
        }

        public static void IsDisplayed(BrowserWrapper wrapper, string selector, Func<string, By> tmpSelectedMethod = null)
        {
            var elements = wrapper.FindElements(selector, tmpSelectedMethod);
            var operationRunner = All(elements);
            var isDisplayedValidator = new IsDisplayedValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(isDisplayedValidator);
        }
        public static void IsNotDisplayed(BrowserWrapper wrapper, string selector, Func<string, By> tmpSelectedMethod = null)
        {
            var elements = wrapper.FindElements(selector, tmpSelectedMethod);
            var operationRunner = All(elements);
            var isNotDisplayedValidator = new IsNotDisplayedValidator();
            operationRunner.Evaluate<BrowserException>(isNotDisplayedValidator);
        }

        public static void UrlIsAccessible(BrowserWrapper wrapper, string url, UrlKind urlKind)
        {
            var urlIsAccessible = new UrlIsAccessibleValidator(url, urlKind);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, urlIsAccessible);
        }

        public static void TitleEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var titleEquals = new TitleEqualsValidator(title, caseSensitive, trim);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, titleEquals);
        }

        public static void TitleNotEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var titleNotEquals = new TitleNotEqualsValidator(title, caseSensitive, trim);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, titleNotEquals);
        }

        public static void Title(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var title = new TitleValidator(expression, failureMessage);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, title);
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
            OperationValidator.Validate<TException>(operationResult);
        }

        public static AnyOperationRunner<T> Any<T>(IEnumerable<T> wrappers)
        {
            return new AnyOperationRunner<T>(wrappers);
        }

        public static AllOperationRunner<T> All<T>(IEnumerable<T> elementWrappers)
        {
            return new AllOperationRunner<T>(elementWrappers);
        }
    }
}
