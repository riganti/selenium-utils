using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class CheckIfAlertText : ICheck<IBrowserWrapper>
    {
        private readonly string failureMessage;

        private readonly Expression<Func<string, bool>> expression;

        public CheckIfAlertText(Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            this.expression = expression;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var alert = wrapper.Driver.SwitchTo().Alert()?.Text;

            var isSucceeded = expression.Compile()(alert);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Alert text is not correct. Provided value: '{alert}' \n { failureMessage } ");
        }
    }
}