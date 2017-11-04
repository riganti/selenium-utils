using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class IsNotEnabledValidator : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsEnabled();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is enabled and should not be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}