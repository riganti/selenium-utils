using System;
using System.Linq;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class ValidatorAttributeValidator : IValidator<IElementWrapper>
    {
        private readonly string[] allowedValues;
        private readonly string attributeName;
        private bool caseSensitive;
        private bool trimValue;
        private readonly string failureMessage;

        public ValidatorAttributeValidator(string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            this.allowedValues = new[] {value};
            this.attributeName = attributeName;
            this.caseSensitive = caseSensitive;
            this.trimValue = trimValue;
            this.failureMessage = failureMessage;
        }

        public ValidatorAttributeValidator(string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {

            this.allowedValues = allowedValues;
            this.attributeName = attributeName;
            this.caseSensitive = caseSensitive;
            this.trimValue = trimValue;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            string[] tempAllowedValues = allowedValues;
            var attribute = wrapper.WebElement.GetAttribute(attributeName);
            if (trimValue)
            {
                attribute = attribute.Trim();
                tempAllowedValues = allowedValues.Select(s => s.Trim()).ToArray();
            }
            var isSucceeded = tempAllowedValues.Any(v => string.Equals(v, attribute,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
                
            if (!isSucceeded){
                return new CheckResult(failureMessage ?? $"Attribute contains unexpected value. Expected value: '{(tempAllowedValues.Length == 1 ? tempAllowedValues[0] : string.Concat("|", tempAllowedValues))}', Provided value: '{attribute}' \r\n Element selector: {wrapper.FullSelector} \r\n");
            }
            return CheckResult.Succeeded;
        }
    }
}