using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class DoesNotContainTextValidator : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = !string.IsNullOrWhiteSpace(wrapper.GetInnerText());
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element does contain text. Element should be empty.\r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}