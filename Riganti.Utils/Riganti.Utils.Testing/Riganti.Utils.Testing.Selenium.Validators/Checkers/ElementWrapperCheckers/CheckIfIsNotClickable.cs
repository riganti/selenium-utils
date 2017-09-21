using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsNotClickable : ICheck<IElementWrapper>
    {
        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsClickable();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"The element '{wrapper.FullSelector}' is clickable and should not be.");
        }
    }
}