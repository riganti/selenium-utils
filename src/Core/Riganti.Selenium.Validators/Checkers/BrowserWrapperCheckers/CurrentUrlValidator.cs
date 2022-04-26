using System;
using System.Linq.Expressions;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class CurrentUrlValidator : IValidator<IBrowserWrapper>
    {
        private readonly string failureMessage;
        private readonly Expression<Func<string, bool>> expression;

        public CurrentUrlValidator(Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            this.expression = expression;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var isSucceeded = expression.Compile()(wrapper.CurrentUrl);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}'. " + (failureMessage ?? ""));
        }
    }
}