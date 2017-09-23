using System;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckIfInnerTextEquals : ICheck<IElementWrapper>
    {
        private readonly string text;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public CheckIfInnerTextEquals(string text, bool caseSensitive = false, bool trim = true)
        {
            this.text = text;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var innerText = trim ? wrapper.GetInnerText()?.Trim() : wrapper.GetInnerText();
            var isSucceeded = string.Equals(innerText, text,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains wrong content. Expected content: '{text}', Provided content: '{innerText}' \r\n Element selector: {wrapper.FullSelector} \r\n");
        }
    }
}