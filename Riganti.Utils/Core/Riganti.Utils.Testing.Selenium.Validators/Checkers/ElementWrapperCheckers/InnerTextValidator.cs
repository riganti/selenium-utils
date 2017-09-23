using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class InnerTextValidator : ICheck<IElementWrapper>
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;

        public InnerTextValidator(Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var innerText = wrapper.GetInnerText();
            var isSucceeded = rule.Compile()(innerText);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains wrong content. Provided content: '{innerText}' \r\n Element selector: {wrapper.FullSelector} \r\n {failureMessage ?? ""}");
        }
    }
}
