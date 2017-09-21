using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class CheckIfTitle : ICheck<IBrowserWrapper>
    {
        private readonly string failureMessage;
        private readonly Expression<Func<string, bool>> expression;

        public CheckIfTitle(Expression<Func<string, bool>> expression, string failureMessage = "")
        {
            this.expression = expression;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var browserTitle = wrapper.GetTitle();
            
            var isSucceeded = expression.Compile()(browserTitle);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Provided content in tab's title is not expected. Provided content: '{browserTitle}' \r\n{failureMessage}");
        }
    }
}