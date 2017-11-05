using System;
using System.Linq.Expressions;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class JsPropertyInnerTextValidator : IValidator<IElementWrapper>
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;
        private readonly bool trim;

        public JsPropertyInnerTextValidator(Expression<Func<string, bool>> rule, string failureMessage = null, bool trim = true)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
            this.trim = trim;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var jsInnerText = wrapper.GetJsInnerText(trim);
            var isSucceeded = rule.Compile()(jsInnerText);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerText property of element. Provided content: '{jsInnerText}' \r\n Element selector: {wrapper.FullSelector} \r\n{failureMessage ?? ""}");
        }
    }
}