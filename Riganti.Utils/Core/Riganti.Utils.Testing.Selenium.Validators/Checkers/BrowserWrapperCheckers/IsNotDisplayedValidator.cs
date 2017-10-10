using System;
using System.Linq;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    /// <summary>
    /// TODO: REMOVE THIS VALIDATOR
    /// Ladislav Schumacher
    /// </summary>

    public class IsNotDisplayedValidator : ICheck<IBrowserWrapper>
    {
        private readonly string selector;

        private readonly Func<string, By> tmpSelectMethod;

        public IsNotDisplayedValidator(string selector, Func<string, By> tmpSelectMethod = null)
        {
            this.tmpSelectMethod = tmpSelectMethod;
            this.selector = selector;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var collection = wrapper.FindElements(selector, tmpSelectMethod);

            var isSucceeded = !collection.ThrowIfSequenceEmpty().All(s => s.IsDisplayed());

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"One or more elements are not displayed. Selector '{selector}', Index of non-displayed element: " +
                                                                         $"{(collection.Any() ? collection.IndexOf(collection.First(s => !s.IsDisplayed())) : -1)}");
        }
    }
}