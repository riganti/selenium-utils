using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers;
using Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers;
using HyperLinkEquals = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers.HyperLinkEqualsValidator;
using IsDisplayed = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers.IsDisplayedValidator;
using IsNotDisplayed = Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers.IsNotDisplayedValidator;
using Riganti.Utils.Testing.Selenium.Validators;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public static class OperationRunnerExtensions
    {
        public static void InnerText(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule)
        {
            var InnerText = new InnerTextValidator(rule);
            operationRunner.Evaluate<UnexpectedElementStateException>(InnerText);
        }

        public static void Value(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false, bool trim = true)
        {
            var Value = new ValueValidator(value, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(Value);
        }

        public static void InnerTextEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var InnerTextEquals = new InnerTextEqualsValidator(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(InnerTextEquals);
        }

        public static void Text(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            var Text = new TextValidator(rule, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(Text);
        }

        public static void TextEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var TextEquals = new TextEqualsValidator(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(TextEquals);
        }
        public static void TextNotEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true, string failureMessage = null)
        {
            var TextNotEquals = new TextNotEqualsValidator(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(TextNotEquals);
        }

        public static void IsDisplayed(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsDisplayed = new IsDisplayed();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsDisplayed);
        }

        public static void IsNotDisplayed(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsNotDisplayed = new IsNotDisplayed();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsNotDisplayed);
        }

        public static void IsChecked(this IOperationRunner<IElementWrapper> operationRunner)
        {
            operationRunner.CheckTagName("input", "Function IsNotChecked() can be used on input element only.");
            operationRunner.CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox.");

            var IsChecked = new IsCheckedValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsChecked);
        }

        public static void IsNotChecked(this IOperationRunner<IElementWrapper> operationRunner)
        {
            operationRunner.CheckTagName("input", "Function IsNotChecked() can be used on input element only.");
            operationRunner.CheckAttribute("type", new[] { "checkbox", "radio" }, failureMessage: "Input element must be type of checkbox or radio.");

            var IsNotChecked = new IsNotCheckedValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsNotChecked);
        }

        public static void IsSelected(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsSelected = new IsSelectedValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsSelected);
        }

        public static void IsNotSelected(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsNotSelected = new IsNotSelectedValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsNotSelected);
        }

        public static void IsEnabled(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsEnabled = new IsEnabledValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsEnabled);
        }

        public static void IsClickable(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsClickable = new IsClickableValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsClickable);
        }

        public static void IsNotClickable(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsNotClickable = new IsNotClickableValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsNotClickable);
        }

        public static void IsNotEnabled(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var IsNotEnabled = new IsNotEnabledValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(IsNotEnabled);
        }

        public static void ContainsText(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var ContainsText = new ContainsTextValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(ContainsText);
        }
        public static void DoesNotContainsText(this IOperationRunner<IElementWrapper> operationRunner)
        {
            var DoesNotContainsText = new DoesNotContainTextValidator();
            operationRunner.Evaluate<UnexpectedElementStateException>(DoesNotContainsText);
        }

        public static void HyperLineEquals(this IOperationRunner<IElementWrapper> operationRunner, string url, UrlKind kind, params UriComponents[] components)
        {
            var HyperLinkEquals = new HyperLinkEquals(url, kind, components);
            operationRunner.Evaluate<UnexpectedElementStateException>(HyperLinkEquals);
        }

        public static void IsElementInView(this IOperationRunner<IElementWrapper> operationRunner, ElementWrapper element)
        {
            var IsElementInView = new IsElementInViewValidator(element);
            operationRunner.Evaluate<UnexpectedElementStateException>(IsElementInView);
        }

        public static void IsElementNotInView(this IOperationRunner<IElementWrapper> operationRunner, ElementWrapper element)
        {
            var IsElementNotInView = new IsElementNotInViewValidator(element);
            operationRunner.Evaluate<UnexpectedElementStateException>(IsElementNotInView);
        }

        public static void CheckTagName(this IOperationRunner<IElementWrapper> operationRunner, string expectedTagName, string failureMessage = null)
        {
            operationRunner.TagName(expectedTagName, failureMessage);
        }

        public static void TagName(this IOperationRunner<IElementWrapper> operationRunner, string expectedTagName, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagName, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkTagName);
        }

        public static void TagName(this IOperationRunner<IElementWrapper> operationRunner, string[] expectedTagNames, string failureMessage = null)
        {
            var checkTagName = new TagNameValidator(expectedTagNames, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(checkTagName);
        }

        //public static void TagNameValidator(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMessage = null)
        //{
        //    operationRunner.TagName(rule, failureMessage);
        //}

        //public static void TagName(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMessage = null)
        //{
        //    var TagName = new TagName(rule, failureMessage, operationRunner);
        //    operationRunner.Evaluate<UnexpectedElementStateException>(TagName);
        //}


        public static void ContainsElement(this IOperationRunner<IElementWrapper> operationRunner, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var ContainsElement = new ContainsElementValidator(cssSelector, tmpSelectMethod);
            operationRunner.Evaluate<EmptySequenceException>(ContainsElement);
        }

        public static void NotContainsElement(this IOperationRunner<IElementWrapper> operationRunner, string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            var NotContainsElement = new NotContainsElementValidator(cssSelector, tmpSelectMethod);
            operationRunner.Evaluate<MoreElementsInSequenceException>(NotContainsElement);
        }

        public static void JsPropertyInnerText(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> rule, string failureMesssage = null, bool trim = true)
        {
            var JsPropertyInnerText = new JsPropertyInnerTextValidator(rule, failureMesssage, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(JsPropertyInnerText);
        }

        public static void JsPropertyInnerTextEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true)
        {
            var JsPropertyInnerTextEquals = new JsPropertyInnerTextEqualsValidator(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(JsPropertyInnerTextEquals);
        }

        public static void JsPropertyInnerHtmlEquals(this IOperationRunner<IElementWrapper> operationRunner, string text, bool caseSensitive = false, bool trim = true)
        {
            var JsPropertyInnerHtmlEquals = new JsPropertyInnerHtmlEqualsValidator(text, caseSensitive, trim);
            operationRunner.Evaluate<UnexpectedElementStateException>(JsPropertyInnerHtmlEquals);
        }

        public static void JsPropertyInnerHtml(this IOperationRunner<IElementWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var JsPropertyInnerHtml = new JsPropertyInnerHtmlValidator(expression, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(JsPropertyInnerHtml);
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
            var Attribute = new AttributeValidator(attributeName, expression, failureMessage);
            operationRunner.Evaluate<UnexpectedElementStateException>(Attribute);
        }

        public static void CheckClassAttribute(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false, bool trimValue = true)
        {
            operationRunner.CheckAttribute("class", value, caseSensitive, trimValue);
        }

        public static void CheckClassAttribute(this IOperationRunner<IElementWrapper> operationRunner, string attributeName, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            operationRunner.CheckAttribute("class", expression, failureMessage);
        }

        public static void HasClass(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false)
        {
            operationRunner.CheckAttribute("class", p => p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasNotClass(this IOperationRunner<IElementWrapper> operationRunner, string value, bool caseSensitive = false)
        {
            operationRunner.CheckAttribute("class", p => !p.Split(' ').Any(c => string.Equals(c, value,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)), $"Expected value: '{value}'.");
        }

        public static void HasAttribute(this IOperationRunner<IElementWrapper> operationRunner, string name)
        {
            var HasAttribute = new HasAttributeValidator(name);
            operationRunner.Evaluate<UnexpectedElementStateException>(HasAttribute);
        }

        public static void HasNotAttribute(this IOperationRunner<IElementWrapper> operationRunner, string name)
        {
            var HasNotAttribute = new HasNotAttributeValidator(name);
            operationRunner.Evaluate<UnexpectedElementStateException>(HasNotAttribute);
        }
        public static void AlertTextEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string expectedValue,
            bool caseSensitive = false, bool trim = true)
        {
            var AlertTextEquals = new AlertTextContainsValidator(expectedValue, caseSensitive, trim);
            operationRunner.Evaluate<AlertException>(AlertTextEquals);
        }

        public static void AlertText(this IOperationRunner<IBrowserWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var AlertText = new AlertTextValidator(expression, failureMessage);
            operationRunner.Evaluate<AlertException>(AlertText);
        }

        public static void AlertTextContains(this IOperationRunner<IBrowserWrapper> operationRunner, string expectedValue, bool trim = true)
        {
            var AlertTextContains = new AlertTextContainsValidator(expectedValue, trim);
            operationRunner.Evaluate<AlertException>(AlertTextContains);
        }

        public static void CheckUrl(this IOperationRunner<IBrowserWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            var Url = new UrlValidator(expression, failureMessage);
            operationRunner.Evaluate<BrowserLocationException>(Url);
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

        public static void HyperLinkEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            var HyperLinkEquals = new Validators.Checkers.BrowserWrapperCheckers.HyperLinkEqualsValidator(selector, url, kind, components);
            operationRunner.Evaluate<UnexpectedElementStateException>(HyperLinkEquals);
        }

        //public static void IsDisplayed(this IOperationRunner<IBrowserWrapper> operationRunner, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        //{
        //    var IsDisplayed = new IsDisplayed();
        //    operationRunner.Evaluate<UnexpectedElementStateException>(IsDisplayed);
        //}
        //public static void IsNotDisplayed(this IOperationRunner<IBrowserWrapper> operationRunner, string selector, Expression<Func<string, By>> tmpSelectedMethod = null)
        //{
        //    var IsNotDisplayed = new Checkers.BrowserWrapperCheckers.IsNotDisplayed(selector, tmpSelectedMethod);
        //    operationRunner.Evaluate<BrowserException>(IsNotDisplayed);
        //}

        public static void UrlIsAccessible(this IOperationRunner<IBrowserWrapper> operationRunner, string url, UrlKind urlKind)
        {
            var UrlIsAccessible = new UrlIsAccessibleValidator(url, urlKind);
            operationRunner.Evaluate<BrowserException>(UrlIsAccessible);
        }

        public static void TitleEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string title, bool caseSensitive = false, bool trim = true)
        {
            var TitleEquals = new TitleEqualsValidator(title, caseSensitive, trim);
            operationRunner.Evaluate<BrowserException>(TitleEquals);
        }

        public static void TitleNotEquals(this IOperationRunner<IBrowserWrapper> operationRunner, string title, bool caseSensitive = false, bool trim = true)
        {
            var TitleNotEquals = new TitleNotEqualsValidator(title, caseSensitive, trim);
            operationRunner.Evaluate<BrowserException>(TitleNotEquals);
        }

        public static void Title(this IOperationRunner<IBrowserWrapper> operationRunner, Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            var Title = new TitleValidator(expression, failureMessage);
            operationRunner.Evaluate<BrowserException>(Title);
        }


        public static void Check<TException, T>(this IOperationRunner<T> operationRunner, ICheck<T> check)
            where TException : TestExceptionBase, new()
        {
            operationRunner.Evaluate<TException>(check);
        }
    }
}
