using System;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfValue : ICheck
    {
        private readonly string value;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public CheckIfValue(string value, bool caseSensitive = false, bool trim = true)
        {
            this.value = value;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var tagName = wrapper.GetTagName();
            var tempValue = value;
            string elementValue = null;

            if (tagName == "input")
            {
                elementValue = wrapper.WebElement.GetAttribute("value");
            }

            if (tagName == "textarea")
            {
                elementValue = wrapper.GetInnerText();
            }


            if (trim)
            {
                elementValue = elementValue?.Trim();
                tempValue = tempValue?.Trim();
            }

            var isSucceeded = string.Equals(tempValue, elementValue,
                caseSensitive ?  StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Attribute contains unexpected value. Expected value: '{tempValue}', Provided value: '{elementValue}' \r\n Element selector: {wrapper.FullSelector} \r\n");
        }
    }
}