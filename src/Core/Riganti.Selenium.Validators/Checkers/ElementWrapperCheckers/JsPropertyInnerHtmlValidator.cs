using System;
using System.Linq.Expressions;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class JsPropertyInnerHtmlValidator : ICheck<IElementWrapper>
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;

        public JsPropertyInnerHtmlValidator(Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var jsInnerHtml = wrapper.GetJsInnerHtml();
            var isSucceeded = rule.Compile()(jsInnerHtml);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerHTML property of element. Provided content: '{jsInnerHtml}' \r\n Element selector: {wrapper.FullSelector} \r\n{failureMessage ?? ""}");
        }
    }
}