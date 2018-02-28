using System;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CssStyleValidator : IValidator<IElementWrapper>
    {
        private readonly string value;
        private readonly string styleName;
        private bool caseSensitive;
        private bool trimValue;
        private readonly string failureMessage;

        public CssStyleValidator(string styleName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            this.value = value;
            this.styleName = styleName;
            this.caseSensitive = caseSensitive;
            this.trimValue = trimValue;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var styleValue = wrapper.WebElement.GetCssValue(styleName);
            var expectedValue = value;
            if (trimValue)
            {
                styleValue = styleValue.Trim();
                expectedValue = expectedValue.Trim();
            }
            var isSucceeded = string.Equals(expectedValue, styleValue,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            if (!isSucceeded)
            {
                return new CheckResult(failureMessage ?? $"Css Style contains unexpected value. Expected value: '{expectedValue}', Provided value: '{styleValue}' \r\n Element selector: {wrapper.FullSelector} \r\n");
            }
            return CheckResult.Succeeded;
        }
    }
}