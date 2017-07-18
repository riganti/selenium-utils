using System;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckIfAlertTextEquals : ICheck<BrowserWrapper>
    {
        private readonly string expectedValue;
        private readonly bool caseSensitive;
        private readonly bool trim;

        public CheckIfAlertTextEquals(string expectedValue, bool caseSensitive = false, bool trim = true)
        {
            this.expectedValue = expectedValue;
            this.caseSensitive = caseSensitive;
            this.trim = trim;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            var alert = wrapper.GetAlert();
            string alertText = alert.Text;
            var tempExpectedValue = expectedValue;
            if (trim)
            {
                alertText = alert.Text?.Trim();
                tempExpectedValue = expectedValue.Trim();
            }

            var isSucceeded = string.Equals(alertText, tempExpectedValue,
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Alert does not contain expected value. Expected value: '{tempExpectedValue}', provided value: '{alertText}'");
        }
    }
}