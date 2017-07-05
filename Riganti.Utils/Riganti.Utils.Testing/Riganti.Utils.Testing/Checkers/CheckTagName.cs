using System;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckTagName : ICheck
    {
        private readonly string[] expectedTagNames;
        private readonly string failureMessage;

        public CheckTagName(string extpectedTagName, string failureMessage = null)
        {
            this.expectedTagNames = new [] {extpectedTagName};
            this.failureMessage = failureMessage;
        }

        public CheckTagName(string[] extpectedTagNames, string failureMessage = null)
        {
            this.expectedTagNames = extpectedTagNames;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSuceeded = false;

            foreach (var expectedTagName in expectedTagNames)
            {
                if (string.Equals(wrapper.GetTagName(), expectedTagName, StringComparison.OrdinalIgnoreCase))
                {
                    isSuceeded = true;
                }
            }


            if (!isSuceeded)
            {
                var allowed = string.Join(", ", expectedTagNames);
                return new CheckResult(failureMessage ?? $"Element has wrong tagName. Expected value: '{allowed}', Provided value: '{wrapper.GetTagName()}' \r\n Element selector: {wrapper.Selector} \r\n");
            }
            return CheckResult.Succeeded;
        }
    }
}