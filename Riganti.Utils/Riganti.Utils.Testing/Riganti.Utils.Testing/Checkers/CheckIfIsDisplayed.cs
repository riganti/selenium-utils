using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsDisplayed : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsDisplayed();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not displayed. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}