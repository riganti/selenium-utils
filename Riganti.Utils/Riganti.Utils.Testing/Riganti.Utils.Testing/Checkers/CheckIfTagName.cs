using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfTagName : ICheck
    {
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;

        public CheckIfTagName(Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var tagName = wrapper.GetTagName();
            var isSucceeded = rule.Compile()(tagName);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element has wrong tagName. Provided value: '{tagName}' \r\n Element selector: {wrapper.Selector} \r\n { (failureMessage ?? "")}");
        }
    }
}