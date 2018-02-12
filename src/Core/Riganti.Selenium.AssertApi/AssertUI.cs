using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Api;
using Riganti.Selenium.Validators.Checkers;
using Riganti.Selenium.Validators.Checkers.BrowserWrapperCheckers;
using Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core;

namespace Riganti.Selenium.Core
{
    public static class AssertUI
    {
        private static readonly OperationResultValidator OperationResultValidator = new OperationResultValidator();

        public static void InnerText(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var innerText = new InnerTextValidator(rule, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, innerText);
        }

        public static void InnerTextEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var innerTextEquals = new InnerTextEqualsValidator(text, caseSensitive, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, innerTextEquals);
        }

        public static void Text(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var text = new TextValidator(rule, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, text);
        }

        public static void TextEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var textEquals = new TextEqualsValidator(text, caseSensitive, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, textEquals);
        }
        public static void TextNotEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var textNotEquals = new TextNotEqualsValidator(text, caseSensitive, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, textNotEquals);
        }

        public static void IsDisplayed(IElementWrapper wrapper)
        {
            var isDisplayed = new IsDisplayedValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isDisplayed);
        }

        public static void IsNotDisplayed(IElementWrapper wrapper)
        {
            var isNotDisplayed = new IsNotDisplayedValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotDisplayed);
        }

        public static void IsChecked(IElementWrapper wrapper)
        {
            TagName(wrapper, "input", "Function IsNotChecked() can be used on input element only.");
            Attribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var isChecked = new IsCheckedValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isChecked);
        }

        public static void IsNotChecked(IElementWrapper wrapper)
        {
            TagName(wrapper, "input", "Function IsNotChecked() can be used on input element only.");
            Attribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var isNotChecked = new IsNotCheckedValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotChecked);
        }

        public static void IsSelected(IElementWrapper wrapper)
        {
            var isSelected = new IsSelectedValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isSelected);
        }

        public static void IsNotSelected(IElementWrapper wrapper)
        {
            var isNotSelected = new IsNotSelectedValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotSelected);
        }

        public static void IsEnabled(IElementWrapper wrapper)
        {
            var isEnabled = new IsEnabledValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isEnabled);
        }

        public static void IsClickable(IElementWrapper wrapper)
        {
            var isClickable = new IsClickableValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isClickable);
        }

        public static void IsNotClickable(IElementWrapper wrapper)
        {
            var isNotClickable = new IsNotClickableValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotClickable);
        }

        public static void IsNotEnabled(IElementWrapper wrapper)
        {
            var isNotEnabled = new IsNotEnabledValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isNotEnabled);
        }

        public static void Value(IElementWrapper wrapper, string value, bool caseSensitive = false, bool trim = true)
        {
            var Value = new ValueValidator(value, caseSensitive, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, Value);
        }

        public static void ContainsText(IElementWrapper wrapper)
        {
            var containsText = new ContainsTextValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, containsText);
        }
        public static void TextEmpty(IElementWrapper wrapper)
        {
            var doesNotContainsText = new TextEmptyValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, doesNotContainsText);
        }
        public static void TextNotEmpty(IElementWrapper wrapper)
        {
            var doesNotContainsText = new TextNotEmptyValidator();
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, doesNotContainsText);
        }

        public static void HyperLinkEquals(IElementWrapper wrapper, string url, UrlKind kind, params UriComponents[] components)
        {
            var hyperLinkEquals = new HyperLinkEqualsValidator(url, kind, components);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, hyperLinkEquals);
        }

        public static void IsElementInView(IElementWrapper wrapper, ElementWrapper element)
        {
            var isElementInView = new IsElementInViewValidator(element);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isElementInView);
        }

        public static void IsElementNotInView(IElementWrapper wrapper, ElementWrapper element)
        {
            var isElementNotInView = new IsElementNotInViewValidator(element);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, isElementNotInView);
        }



        public static void TagName(IElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagName, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void TagName(IElementWrapper wrapper, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagNames, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }



        public static void TagName(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var tagName = new TagNameValidator(rule, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, tagName);
        }


        public static void ContainsElement(IElementWrapper wrapper, string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            var containsElement = new ContainsElementValidator(cssSelector, tmpSelectMethod);
            EvaluateValidator<EmptySequenceException, IElementWrapper>(wrapper, containsElement);
        }

        public static void NotContainsElement(IElementWrapper wrapper, string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            var notContainsElement = new NotContainsElementValidator(cssSelector, tmpSelectMethod);
            EvaluateValidator<MoreElementsInSequenceException, IElementWrapper>(wrapper, notContainsElement);
        }

        public static void JsPropertyInnerText(IElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var jsPropertyInnerText = new JsPropertyInnerTextValidator(rule, failureMesssage, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerText);
        }

        public static void JsPropertyInnerTextEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var jsPropertyInnerTextEquals = new JsPropertyInnerTextEqualsValidator(text, caseSensitive, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerTextEquals);
        }

        public static void JsPropertyInnerHtmlEquals(IElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var jsPropertyInnerHtmlEquals = new JsPropertyInnerHtmlEqualsValidator(text, caseSensitive, trim);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerHtmlEquals);
        }

        public static void JsPropertyInnerHtml(IElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var jsPropertyInnerHtml = new JsPropertyInnerHtmlValidator(expression, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, jsPropertyInnerHtml);
        }

        public static void Attribute(IElementWrapper wrapper, string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new ValidatorAttributeValidator(attributeName, value, caseSensitive, trimValue, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, checkAttribute);
        }

        public static void Attribute(IElementWrapper wrapper, string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            var checkAttribute = new ValidatorAttributeValidator(attributeName, allowedValues, caseSensitive, trimValue, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, checkAttribute);
        }

        public static void Attribute(IElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var attribute = new AttributeValidator(attributeName, expression, failureMessage);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, attribute);
        }

        public static void ClassAttribute(IElementWrapper wrapper, string value, bool caseSensitive = true, bool trimValue = true)
        {
            Attribute(wrapper, "class", value, caseSensitive, trimValue);
        }

        public static void ClassAttribute(IElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            Attribute(wrapper, "class", expression, failureMessage);
        }

        public static void HasClass(IElementWrapper wrapper, string value, bool caseSensitive = true)
        {
            var values = value.Split(' ');
            Attribute(wrapper, "class", p => values.All(v => p.Split(' ').Any(c => string.Equals(c, v,
               caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))), $"Expected value: '{value}'.");
        }

        public static void HasNotClass(IElementWrapper wrapper, string value, bool caseSensitive = true)
        {
            Attribute(wrapper, "class", p => !p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasAttribute(IElementWrapper wrapper, string name)
        {
            var hasAttribute = new HasAttributeValidator(name);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, hasAttribute);
        }

        public static void HasNotAttribute(IElementWrapper wrapper, string name)
        {
            var hasNotAttribute = new HasNotAttributeValidator(name);
            EvaluateValidator<UnexpectedElementStateException, IElementWrapper>(wrapper, hasNotAttribute);
        }

        public static void AlertTextEquals(IBrowserWrapper wrapper, string expectedValue,
            bool caseSensitive = false, bool trim = true)
        {
            var alertTextEquals = new AlertTextEqualsValidator(expectedValue, caseSensitive, trim);
            EvaluateValidator<AlertException, IBrowserWrapper>(wrapper, alertTextEquals);
        }

        public static void AlertText(IBrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var alertText = new AlertTextValidator(expression, failureMessage);
            EvaluateValidator<AlertException, IBrowserWrapper>(wrapper, alertText);
        }

        public static void AlertTextContains(IBrowserWrapper wrapper, string expectedValue, bool trim = true)
        {
            var alertTextContains = new AlertTextContainsValidator(expectedValue, trim);
            EvaluateValidator<AlertException, IBrowserWrapper>(wrapper, alertTextContains);
        }

        public static void Url(IBrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var url = new CurrentUrlValidator(expression, failureMessage);
            EvaluateValidator<BrowserLocationException, IBrowserWrapper>(wrapper, url);
        }

        public static void Url(IBrowserWrapper wrapper, string url, UrlKind urlKind, params UriComponents[] components)
        {
            var checkUrl = new UrlValidator(url, urlKind, components);
            EvaluateValidator<BrowserLocationException, IBrowserWrapper>(wrapper, checkUrl);
        }

        public static void UrlEquals(IBrowserWrapper wrapper, string url)
        {
            var checkUrlExquals = new UrlEqualsValidator(url);
            EvaluateValidator<BrowserLocationException, IBrowserWrapper>(wrapper, checkUrlExquals);
        }

        public static void HyperLinkEquals(IBrowserWrapper wrapper, string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var elements = wrapper.FindElements(selector);
            var operationRunner = All(elements);
            var hyperLinkEquals = new HyperLinkEqualsValidator(url, kind, components);
            operationRunner.Evaluate<UnexpectedElementStateException>(hyperLinkEquals);
        }

        public static void IsDisplayed(IBrowserWrapper wrapper, string selector, Func<string, By> tmpSelectedMethod = null)
        {
            var elements = wrapper.FindElements(selector, tmpSelectedMethod);
            var operationRunner = All(elements);
            var isDisplayedValidator = new IsDisplayedValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(isDisplayedValidator);
        }
        public static void IsNotDisplayed(IBrowserWrapper wrapper, string selector, Func<string, By> tmpSelectedMethod = null)
        {
            var elements = wrapper.FindElements(selector, tmpSelectedMethod);
            var operationRunner = All(elements);
            var isNotDisplayedValidator = new IsNotDisplayedValidator();
            operationRunner.Evaluate<BrowserException>(isNotDisplayedValidator);
        }

        public static void UrlIsAccessible(IBrowserWrapper wrapper, string url, UrlKind urlKind)
        {
            var urlIsAccessible = new UrlIsAccessibleValidator(url, urlKind);
            EvaluateValidator<BrowserException, IBrowserWrapper>(wrapper, urlIsAccessible);
        }

        public static void TitleEquals(IBrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var titleEquals = new TitleEqualsValidator(title, caseSensitive, trim);
            EvaluateValidator<BrowserException, IBrowserWrapper>(wrapper, titleEquals);
        }

        public static void TitleNotEquals(IBrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var titleNotEquals = new TitleNotEqualsValidator(title, caseSensitive, trim);
            EvaluateValidator<BrowserException, IBrowserWrapper>(wrapper, titleNotEquals);
        }

        public static void Title(IBrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var title = new TitleValidator(expression, failureMessage);
            EvaluateValidator<BrowserException, IBrowserWrapper>(wrapper, title);
        }

        public static void Check<TException, T>(IValidator<T> validator, T wrapper)
            where TException : TestExceptionBase, new() where T : ISupportedByValidator
        {
            EvaluateValidator<TException, T>(wrapper, validator);
        }

        private static void EvaluateValidator<TException, T>(T wrapper, IValidator<T> validator)
            where TException : TestExceptionBase, new() where T : ISupportedByValidator
        {
            var operationResult = validator.Validate(wrapper);
            OperationResultValidator.Validate<TException>(operationResult);
        }

        public static AnyOperationRunner<T> Any<T>(IEnumerable<T> wrappers) where T : ISupportedByValidator
        {
            return new AnyOperationRunner<T>(wrappers);
        }

        public static AllOperationRunner<T> All<T>(IEnumerable<T> elementWrappers) where T : ISupportedByValidator
        {
            return new AllOperationRunner<T>(elementWrappers);
        }
    }
}
