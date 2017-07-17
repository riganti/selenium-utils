using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfNotContainsElement : ICheck
    {
        public readonly string cssSelector;
        public readonly Expression<Func<string, By>> tmpSelectMethod;

        public CheckIfNotContainsElement(string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            this.cssSelector = cssSelector;
            this.tmpSelectMethod = tmpSelectMethod;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var count = wrapper.FindElements(cssSelector, tmpSelectMethod?.Compile()).Count;
            if (count != 0)
            {
                var children = count == 1 ? "child" : $"children ({count})";
                return new CheckResult($"This element ('{wrapper.FullSelector}') contains {children} selectable by '{cssSelector}' and should not.");
            }
            return CheckResult.Succeeded;
        }
    }
}