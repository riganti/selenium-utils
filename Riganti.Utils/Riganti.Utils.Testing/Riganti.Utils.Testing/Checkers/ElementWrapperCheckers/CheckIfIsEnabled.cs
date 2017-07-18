using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsEnabled : ICheck<ElementWrapper>
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsEnabled();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not enabled. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}