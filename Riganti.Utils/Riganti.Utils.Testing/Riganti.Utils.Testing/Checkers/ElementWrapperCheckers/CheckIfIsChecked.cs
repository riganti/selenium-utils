using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsChecked : ICheck<ElementWrapper>
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.WebElement.Selected;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is NOT checked and should be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}