using System;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfJsPropertyInnerHtmlEquals : ICheck
    {
        private readonly string text;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public CheckIfJsPropertyInnerHtmlEquals(string text, bool caseSensitive = false, bool trim = true)
        {
            this.text = text;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(ElementWrapper wrapper)
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