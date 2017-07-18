using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfJsPropertyInnerText : ICheck<ElementWrapper>
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;
        private readonly bool trim;

        public CheckIfJsPropertyInnerText(Expression<Func<string, bool>> rule, string failureMessage = null, bool trim = true)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
            this.trim = trim;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var jsInnerText = wrapper.GetJsInnerText(trim);
            var isSucceeded = rule.Compile()(jsInnerText);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains incorrect content in innerText property of element. Provided content: '{jsInnerText}' \r\n Element selector: {wrapper.FullSelector} \r\n{failureMessage ?? ""}");
        }
    }
}