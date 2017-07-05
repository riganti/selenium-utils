using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsChecked : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.WebElement.Selected;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is NOT checked and should be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}