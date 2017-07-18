using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsClickable : ICheck<ElementWrapper>
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsClickable();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"The element '{wrapper.FullSelector}' is not clickable.");
        }
    }
}