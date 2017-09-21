using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsSelected : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsSelected();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not selected. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}