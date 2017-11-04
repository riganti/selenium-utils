using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class IsDisplayedValidator : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsDisplayed();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not displayed. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}