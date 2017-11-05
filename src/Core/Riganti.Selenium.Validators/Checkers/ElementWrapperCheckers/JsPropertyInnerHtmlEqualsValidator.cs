using System;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class JsPropertyInnerHtmlEqualsValidator : IValidator<IElementWrapper>
    {
        private readonly string text;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public JsPropertyInnerHtmlEqualsValidator(string text, bool caseSensitive = false, bool trim = true)
        {
            this.text = text;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var jsInnerHtml = wrapper.GetJsInnerHtml();
            if (trim)
            {
                jsInnerHtml = jsInnerHtml?.Trim();
            }
            var isSucceeded = string.Equals(text, jsInnerHtml,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerHTML property. Expected content: '{text}', Provided content: '{jsInnerHtml}' \r\n Element selector: {wrapper.FullSelector} \r\n");
        }
    }
}