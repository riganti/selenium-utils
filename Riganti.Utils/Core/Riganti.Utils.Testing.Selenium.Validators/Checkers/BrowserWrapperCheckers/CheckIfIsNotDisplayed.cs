using System;
using System.Linq;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class CheckIfIsNotDisplayed : ICheck<IBrowserWrapper>
    {
        private readonly string selector;

        private readonly Expression<Func<string, By>> tmpSelectMethod;

        public CheckIfIsNotDisplayed(string selector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            this.tmpSelectMethod = tmpSelectMethod;
            this.selector = selector;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var collection = wrapper.FindElements(selector, tmpSelectMethod?.Compile());

            var isSucceeded = !collection.ThrowIfSequenceEmpty().All(s => s.IsDisplayed());

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"One or more elements are not displayed. Selector '{selector}', Index of non-displayed element: " +
                                                                         $"{(collection.Any() ? collection.IndexOf(collection.First(s => !s.IsDisplayed())) : -1)}");
        }
    }
}