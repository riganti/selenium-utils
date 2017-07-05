using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsClickable : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsClickable();
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"The element '{wrapper.FullSelector}' is not clickable.");
        }
    }
}