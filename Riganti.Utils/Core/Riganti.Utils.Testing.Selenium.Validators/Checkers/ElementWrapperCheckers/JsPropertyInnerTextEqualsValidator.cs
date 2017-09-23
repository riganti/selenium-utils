using System;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class JsPropertyInnerTextEqualsValidator : ICheck<IElementWrapper>
    {
        private readonly string text;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public JsPropertyInnerTextEqualsValidator(string text, bool caseSensitive = false, bool trim = true)
        {
            this.text = text;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var jsInnerText = wrapper.GetJsInnerText(trim);
            var isSucceeded = string.Equals(jsInnerText, text,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerText/textContent property. Expected content: '{text}', Provided content: '{jsInnerText}' \r\n Element selector: {wrapper.FullSelector} \r\n");
        }
    }
}