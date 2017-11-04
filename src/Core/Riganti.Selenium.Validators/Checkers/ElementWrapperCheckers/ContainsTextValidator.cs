using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class ContainsTextValidator : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = string.IsNullOrWhiteSpace(wrapper.GetInnerText());
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element doesn't contain text. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}