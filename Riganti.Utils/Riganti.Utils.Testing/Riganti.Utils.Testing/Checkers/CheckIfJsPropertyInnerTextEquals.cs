using System;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfJsPropertyInnerTextEquals : ICheck
    {
        private readonly string text;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public CheckIfJsPropertyInnerTextEquals(string text, bool caseSensitive = false, bool trim = true)
        {
            this.text = text;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var jsInnerText = wrapper.GetJsInnerText(trim);
            var isSucceeded = string.Equals(jsInnerText, text,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerText/textContent property. Expected content: '{text}', Provided content: '{jsInnerText}' \r\n Element selector: {wrapper.FullSelector} \r\n");
        }
    }
}