using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsSelected : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsSelected();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not selected. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}