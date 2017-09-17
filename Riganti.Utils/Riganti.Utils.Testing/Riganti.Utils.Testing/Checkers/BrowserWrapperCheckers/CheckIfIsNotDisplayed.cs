using System;
using System.Linq;
using System.Linq.Expressions;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckIfIsNotDisplayed : ICheck<BrowserWrapper>
    {
        private readonly string selector;

        private readonly Expression<Func<string, By>> tmpSelectMethod;

        public CheckIfIsNotDisplayed(string selector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            this.tmpSelectMethod = tmpSelectMethod;
            this.selector = selector;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            var collection = wrapper.FindElements(selector, tmpSelectMethod?.Compile());

            var isSucceeded = !collection.ThrowIfSequenceEmpty().All(s => s.IsDisplayed());

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"One or more elements are not displayed. Selector '{selector}', Index of non-displayed element: " +
                                                                         $"{(collection.Any() ? collection.IndexOf(collection.First(s => !s.IsDisplayed())) : -1)}");
        }
    }
}