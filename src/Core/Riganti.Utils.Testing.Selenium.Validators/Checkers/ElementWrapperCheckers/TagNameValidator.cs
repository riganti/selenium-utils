using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class TagNameValidator : ICheck<IElementWrapper>
    {
        private readonly string[] expectedTagNames;
        private readonly string failureMessage;
        private readonly Expression<Func<string, bool>> rule;

        public TagNameValidator(string extpectedTagName, string failureMessage = null)
        {
            this.expectedTagNames = new[] { extpectedTagName };
            this.failureMessage = failureMessage;
        }

        public TagNameValidator(string[] extpectedTagNames, string failureMessage = null)
        {
            this.expectedTagNames = extpectedTagNames;
            this.failureMessage = failureMessage;
        }
        public TagNameValidator(Expression<Func<string, bool>> rule, string failureMessage = null)
        {
            this.rule = rule;
            this.failureMessage = failureMessage;
        }


        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = false;
            ValidateExpectedNames(wrapper, ref isSucceeded);
            ValidateRule(wrapper, ref isSucceeded);

            if (!isSucceeded)
            {
                var allowed = string.Join(", ", expectedTagNames);
                return new CheckResult(failureMessage ?? $"Element has wrong tagName. Expected value: '{allowed}', Provided value: '{wrapper.GetTagName()}' \r\n Element selector: {wrapper.Selector} \r\n");
            }
            return CheckResult.Succeeded;
        }

        private void ValidateRule(IElementWrapper wrapper, ref bool isSucceeded)
        {
            if (rule != null)
            {
                isSucceeded = rule.Compile()(wrapper.GetTagName());
            }
        }

        private void ValidateExpectedNames(IElementWrapper wrapper, ref bool isSucceeded)
        {
            if (expectedTagNames != null)
            {
                foreach (var expectedTagName in expectedTagNames)
                {
                    if (string.Equals(wrapper.GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
                    {
                        isSucceeded = true;
                    }
                }
            }

        }
    }
}