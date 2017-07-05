using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsNotEnabled : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsEnabled();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is enabled and should not be. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}