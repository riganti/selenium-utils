using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class ContainsElementValidator : ICheck<IElementWrapper>
    {
        public readonly string cssSelector;
        public readonly Expression<Func<string, By>> tmpSelectMethod;

        public ContainsElementValidator(string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            this.cssSelector = cssSelector;
            this.tmpSelectMethod = tmpSelectMethod;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.FindElements(cssSelector, tmpSelectMethod?.Compile()).Count != 0;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"This element ('{wrapper.FullSelector}') does not contain child selectable by '{cssSelector}'.");
        }
    }
}