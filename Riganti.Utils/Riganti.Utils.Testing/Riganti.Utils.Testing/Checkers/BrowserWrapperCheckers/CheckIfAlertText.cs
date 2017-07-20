using System;
using System.Linq.Expressions;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckIfAlertText : ICheck<BrowserWrapper>
    {
        private readonly string failureMessage;

        private readonly Expression<Func<string, bool>> expression;

        public CheckIfAlertText(Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            this.expression = expression;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            var alert = wrapper.Browser.SwitchTo().Alert()?.Text;

            var isSucceeded = expression.Compile()(alert);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Alert text is not correct. Provided value: '{alert}' \n { failureMessage } ");
        }
    }
}