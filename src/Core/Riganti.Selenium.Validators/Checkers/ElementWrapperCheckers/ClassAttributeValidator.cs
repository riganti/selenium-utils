
using System;
using System.Linq;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class ClassAttributeValidator : IValidator<IElementWrapper>
    {
        private readonly string[] allowedValues;
        private const string attributeName = "class";
        private readonly bool caseSensitive;
        private readonly bool trimValue;
        private readonly string failureMessage;
        private readonly bool matchAll;

        public ClassAttributeValidator(string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            this.matchAll = true;
            this.allowedValues = value?.Split(' ');
            this.caseSensitive = caseSensitive;
            this.trimValue = trimValue;
            this.failureMessage = failureMessage;
        }

        public ClassAttributeValidator(string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {

            this.allowedValues = allowedValues;
            this.caseSensitive = caseSensitive;
            this.trimValue = trimValue;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var tempAllowedValues = allowedValues;
            var attribute = (wrapper.WebElement.GetAttribute(attributeName) ?? "").Split(' ');
            if (trimValue)
            {
                attribute = attribute.Select(s => s.Trim()).ToArray();

                tempAllowedValues = allowedValues.Select(s => s.Trim()).ToArray();
            }

            var isSucceeded = matchAll
                ? tempAllowedValues.All(v => attribute.Any(s => string.Equals(v, s, (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))))
                : tempAllowedValues.Any(v => attribute.Any(s => string.Equals(v, s, (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))));

            if (!isSucceeded)
            {
                return new CheckResult(failureMessage ?? $"Attribute contains unexpected value. Expected value: '{(tempAllowedValues.Length == 1 ? tempAllowedValues[0] : string.Concat("|", tempAllowedValues))}', Provided value: '{attribute}' \r\n Element selector: {wrapper.FullSelector} \r\n");
            }
            return CheckResult.Succeeded;
        }
    }
}