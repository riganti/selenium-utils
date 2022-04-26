﻿using System;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class ContainsElementValidator : IValidator<IElementWrapper>
    {
        public readonly string cssSelector;
        public readonly Func<string, By> tmpSelectMethod;

        public ContainsElementValidator(string cssSelector, Func<string, By> tmpSelectMethod = null)
        {
            this.cssSelector = cssSelector;
            this.tmpSelectMethod = tmpSelectMethod;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.FindElements(cssSelector, tmpSelectMethod).Count != 0;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"This element ('{wrapper.FullSelector}') does not contain child selectable by '{cssSelector}'.");
        }
    }
}