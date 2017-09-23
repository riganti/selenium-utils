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

        public static void InnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var InnerText = new InnerTextValidator(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, InnerText);
        }

        public static void InnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var InnerTextEquals = new InnerTextEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, InnerTextEquals);
        }

        public static void Text(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var Text = new TextValidator(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, Text);
        }

        public static void TextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var TextEquals = new TextEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, TextEquals);
        }
        public static void TextNotEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var TextNotEquals = new TextNotEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, TextNotEquals);
        }

        public static void IsDisplayed(ElementWrapper wrapper)
        {
            var IsDisplayed = new  ElementCheckers.IsDisplayedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsDisplayed);
        }

        public static void IsNotDisplayed(ElementWrapper wrapper)
        {
            var IsNotDisplayed = new ElementCheckers.IsNotDisplayedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsNotDisplayed);
        }

        public static void IsChecked(ElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function IsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var IsChecked = new IsCheckedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsChecked);
        }

        public static void IsNotChecked(ElementWrapper wrapper)
        {
            CheckTagName(wrapper, "input", "Function IsNotChecked() can be used on input element only.");
            CheckAttribute(wrapper, "type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var IsNotChecked = new IsNotCheckedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsNotChecked);
        }

        public static void IsSelected(ElementWrapper wrapper)
        {
            var IsSelected = new IsSelectedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsSelected);
        }

        public static void IsNotSelected(ElementWrapper wrapper)
        {
            var IsNotSelected = new IsNotSelectedValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsNotSelected);
        }

        public static void IsEnabled(ElementWrapper wrapper)
        {
            var IsEnabled = new IsEnabledValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsEnabled);
        }

        public static void IsClickable(ElementWrapper wrapper)
        {
            var IsClickable = new IsClickableValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsClickable);
        }

        public static void IsNotClickable(ElementWrapper wrapper)
        {
            var IsNotClickable = new IsNotClickableValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsNotClickable);
        }

        public static void IsNotEnabled(ElementWrapper wrapper)
        {
            var IsNotEnabled = new IsNotEnabledValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsNotEnabled);
        }

        public static void Value(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trim = true)
        {
            var Value = new ValueValidator(value, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, Value);
        }

        public static void ContainsText(ElementWrapper wrapper)
        {
            var ContainsText = new ContainsTextValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, ContainsText);
        }
        public static void DoesNotContainsText(ElementWrapper wrapper)
        {
            var DoesNotContainsText = new DoesNotContainTextValidator();
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, DoesNotContainsText);
        }

        public static void HyperLinkEquals(ElementWrapper wrapper, string url, UrlKind kind, params UriComponents[] components)
        {
            var HyperLinkEquals = new ElementCheckers.HyperLinkEqualsValidator(url, kind, components);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, HyperLinkEquals);
        }

        public static void IsElementInView(ElementWrapper wrapper, ElementWrapper element)
        {
            var IsElementInView = new IsElementInViewValidator(element);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsElementInView);
        }

        public static void IsElementNotInView(ElementWrapper wrapper, ElementWrapper element)
        {
            var IsElementNotInView = new IsElementNotInViewValidator(element);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, IsElementNotInView);
        }

        public static void CheckTagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            TagName(wrapper, expectedTagName, failureMessage);
        }

        public static void TagName(ElementWrapper wrapper, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagName, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void TagName(ElementWrapper wrapper, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagNames, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, checkTagName);
        }

        public static void CheckTagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            TagName(wrapper, rule, failureMessage);
        }

        public static void TagName(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var TagName = new TagNameValidator(rule, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, TagName);
        }


        public static void ContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var ContainsElement = new ContainsElementValidator(cssSelector, tmpSelectMethod);
            EvaluateCheck<EmptySequenceException, IElementWrapper>(wrapper, ContainsElement);
        }

        public static void NotContainsElement(ElementWrapper wrapper, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var NotContainsElement = new NotContainsElementValidator(cssSelector, tmpSelectMethod);
            EvaluateCheck<MoreElementsInSequenceException, IElementWrapper>(wrapper, NotContainsElement);
        }

        public static void JsPropertyInnerText(ElementWrapper wrapper, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var JsPropertyInnerText = new JsPropertyInnerTextValidator(rule, failureMesssage, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, JsPropertyInnerText);
        }

        public static void JsPropertyInnerTextEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var JsPropertyInnerTextEquals = new JsPropertyInnerTextEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, JsPropertyInnerTextEquals);
        }

        public static void JsPropertyInnerHtmlEquals(ElementWrapper wrapper, string text, bool caseSensitive = false, bool trim = true)
        {
            var JsPropertyInnerHtmlEquals = new JsPropertyInnerHtmlEqualsValidator(text, caseSensitive, trim);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, JsPropertyInnerHtmlEquals);
        }

        public static void JsPropertyInnerHtml(ElementWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var JsPropertyInnerHtml = new JsPropertyInnerHtmlValidator(expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, JsPropertyInnerHtml);
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
            var Attribute = new AttributeValidator(attributeName, expression, failureMessage);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, Attribute);
        }

        public static void CheckClassAttribute(ElementWrapper wrapper, string value, bool caseSensitive = false, bool trimValue = true)
        {
            CheckAttribute(wrapper, "class", value, caseSensitive, trimValue);
        }

        public static void CheckClassAttribute(ElementWrapper wrapper, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            CheckAttribute(wrapper, "class", expression, failureMessage);
        }

        public static void HasClass(ElementWrapper wrapper, string value, bool caseSensitive = false)
        {
            CheckAttribute(wrapper, "class", p => p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasNotClass(ElementWrapper wrapper, string value, bool caseSensitive = false)
        {
            CheckAttribute(wrapper, "class", p => !p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasAttribute(ElementWrapper wrapper, string name)
        {
            var HasAttribute = new HasAttributeValidator(name);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, HasAttribute);
        }

        public static void HasNotAttribute(ElementWrapper wrapper, string name)
        {
            var HasNotAttribute = new HasNotAttributeValidator(name);
            EvaluateCheck<UnexpectedElementStateException, IElementWrapper>(wrapper, HasNotAttribute);
        }

        public static void AlertTextEquals(BrowserWrapper wrapper, string expectedValue,
            bool caseSensitive = false, bool trim = true)
        {
            var AlertTextEquals = new AlertTextEqualsValidator(expectedValue, caseSensitive, trim);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, AlertTextEquals);
        }

        public static void AlertText(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var AlertText = new AlertTextValidator(expression, failureMessage);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, AlertText);
        }

        public static void AlertTextContains(BrowserWrapper wrapper, string expectedValue, bool trim = true)
        {
            var AlertTextContains = new AlertTextContainsValidator(expectedValue, trim);
            EvaluateCheck<AlertException, IBrowserWrapper>(wrapper, AlertTextContains);
        }

        public static void CheckUrl(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var Url = new UrlValidator(expression, failureMessage);
            EvaluateCheck<BrowserLocationException, IBrowserWrapper>(wrapper, Url);
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

        public static void HyperLinkEquals(BrowserWrapper wrapper, string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var HyperLinkEquals = new BrowserCheckers.HyperLinkEqualsValidator(selector, url, kind, components);
            EvaluateCheck<UnexpectedElementStateException, IBrowserWrapper>(wrapper, HyperLinkEquals);
        }

        public static void IsDisplayed(BrowserWrapper wrapper, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        {
            var IsDisplayed = new BrowserCheckers.IsDisplayedValidator(selector, tmpSelectedMethod);
            EvaluateCheck<UnexpectedElementStateException, IBrowserWrapper>(wrapper, IsDisplayed);
        }
        public static void IsNotDisplayed(BrowserWrapper wrapper, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        {
            var IsNotDisplayed = new BrowserCheckers.IsNotDisplayedValidator(selector, tmpSelectedMethod);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, IsNotDisplayed);
        }

        public static void UrlIsAccessible(BrowserWrapper wrapper, string url, UrlKind urlKind)
        {
            var UrlIsAccessible = new UrlIsAccessibleValidator(url, urlKind);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, UrlIsAccessible);
        }

        public static void TitleEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var TitleEquals = new TitleEqualsValidator(title, caseSensitive, trim);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, TitleEquals);
        }

        public static void TitleNotEquals(BrowserWrapper wrapper, string title, bool caseSensitive = false, bool trim = true)
        {
            var TitleNotEquals = new TitleNotEqualsValidator(title, caseSensitive, trim);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, TitleNotEquals);
        }

        public static void Title(BrowserWrapper wrapper, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var Title = new TitleValidator(expression, failureMessage);
            EvaluateCheck<BrowserException, IBrowserWrapper>(wrapper, Title);
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
