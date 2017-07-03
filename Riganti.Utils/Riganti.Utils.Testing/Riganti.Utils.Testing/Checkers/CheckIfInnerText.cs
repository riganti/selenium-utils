using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfInnerText : ICheck
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;

        public CheckIfInnerText(Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var innerText = wrapper.GetInnerText();
            var isSucceeded = rule.Compile()(innerText);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element contains wrong content. Provided content: '{wrapper.GetInnerText()}' \r\n Element selector: {wrapper.FullSelector} \r\n {failureMessage ?? ""}");
        }
    }
}
