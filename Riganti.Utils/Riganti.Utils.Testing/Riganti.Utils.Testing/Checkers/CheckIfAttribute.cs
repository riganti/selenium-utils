using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfAttribute : ICheck
    {
        private readonly string attributeName;
        private readonly Expression<Func<string, bool>> rule;
        private readonly string failureMessage;

        public CheckIfAttribute(string attributeName, Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.attributeName = attributeName;
            this.rule = rule;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var attribute = wrapper.WebElement.GetAttribute(attributeName);
            var isSucceeded = rule.Compile()(attribute);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Attribute '{attributeName}' contains unexpected value. Provided value: '{attribute}' \r\n Element selector: {wrapper.FullSelector} \r\n {failureMessage ?? ""}");
        }
    }
}