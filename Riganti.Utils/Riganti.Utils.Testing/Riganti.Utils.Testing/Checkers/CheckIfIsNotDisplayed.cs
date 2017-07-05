using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsNotDisplayed : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsDisplayed();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is displayed and should not be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}