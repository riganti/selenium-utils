using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class NotContainsElementValidator : ICheck<IElementWrapper>
    {
        public readonly string cssSelector;
        public readonly Func<string, By> tmpSelectMethod;

        public NotContainsElementValidator(string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            this.cssSelector = cssSelector;
            this.tmpSelectMethod = tmpSelectMethod;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var count = wrapper.FindElements(cssSelector, tmpSelectMethod).Count;
            if (count != 0)
            {
                var children = count == 1 ? "child" : $"children ({count})";
                return new CheckResult($"This element ('{wrapper.FullSelector}') contains {children} selectable by '{cssSelector}' and should not.");
            }
            return CheckResult.Succeeded;
        }
    }
}