using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsNotDisplayed : ICheck<ElementWrapper>
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsDisplayed();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is displayed and should not be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}