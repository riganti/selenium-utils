using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfContainsElement : ICheck
    {
        public readonly string cssSelector;
        public readonly Expression<Func<string, By>> tmpSelectMethod;

        public CheckIfContainsElement(string cssSelector, Expression<Func<string, By>> tmpSelectMethod = null)
        {
            this.cssSelector = cssSelector;
            this.tmpSelectMethod = tmpSelectMethod;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.FindElements(cssSelector, tmpSelectMethod?.Compile()).Count != 0;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"This element ('{wrapper.FullSelector}') does not contain child selectable by '{cssSelector}'.");
        }
    }
}