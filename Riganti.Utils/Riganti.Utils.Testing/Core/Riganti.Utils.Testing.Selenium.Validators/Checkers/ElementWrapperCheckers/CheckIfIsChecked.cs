using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsChecked : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.WebElement.Selected;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is NOT checked and should be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}