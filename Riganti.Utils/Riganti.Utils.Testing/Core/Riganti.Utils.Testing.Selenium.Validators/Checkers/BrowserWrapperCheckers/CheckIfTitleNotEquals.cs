using System;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class CheckIfTitleNotEquals : ICheck<IBrowserWrapper>
    {
        private readonly string expectedValue;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public CheckIfTitleNotEquals(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            this.expectedValue = expectedValue;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var browserTitle = wrapper.GetTitle();
            var trimExpectedValue = expectedValue;
            if (trim)
            {
                browserTitle = browserTitle.Trim();
                trimExpectedValue = trimExpectedValue.Trim();
            }

            var isSucceeded = !string.Equals(browserTitle, trimExpectedValue,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Provided content in tab's title is not expected. Title should NOT to be equal to '{trimExpectedValue}', but provided value is '{browserTitle}'");
        }
    }
}