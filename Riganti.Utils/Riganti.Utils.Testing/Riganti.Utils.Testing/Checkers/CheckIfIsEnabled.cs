using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsEnabled : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsEnabled();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not enabled. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}