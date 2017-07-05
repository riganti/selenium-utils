using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfJsPropertyInnerHtml : ICheck
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;

        public CheckIfJsPropertyInnerHtml(Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var jsInnerHtml = wrapper.GetJsInnerHtml();
            var isSucceeded = rule.Compile()(jsInnerHtml);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerHTML property of element. Provided content: '{jsInnerHtml}' \r\n Element selector: {wrapper.FullSelector} \r\n{failureMessage ?? ""}");
        }
    }
}