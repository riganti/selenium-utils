using System;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckAttribute : ICheck<IElementWrapper>
    {
        private readonly string[] allowedValues;
        private readonly string attributeName;
        private bool caseSensitive;
        private bool trimValue;
        private readonly string failureMessage;

        public CheckAttribute(string attributeName, string value, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
        {
            this.allowedValues = new[] {value};
            this.attributeName = attributeName;
            this.caseSensitive = caseSensitive;
            this.trimValue = trimValue;
            this.failureMessage = failureMessage;
        }

        public CheckAttribute(string attributeName, string[] allowedValues, bool caseSensitive = false, bool trimValue = true, string failureMessage = null)
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
                tempAllowedValues = Enumerable.Select<string, string>(allowedValues, s => s.Trim()).ToArray();
            }
            var isSuceeded = tempAllowedValues.Any(v => string.Equals(v, attribute,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
                
            if (!isSuceeded){
                return new CheckResult(failureMessage ?? $"Attribute contains unexpected value. Expected value: '{string.Concat("|", tempAllowedValues)}', Provided value: '{attribute}' \r\n Element selector: {wrapper.FullSelector} \r\n");
            }
            return CheckResult.Succeeded;
        }
    }
}