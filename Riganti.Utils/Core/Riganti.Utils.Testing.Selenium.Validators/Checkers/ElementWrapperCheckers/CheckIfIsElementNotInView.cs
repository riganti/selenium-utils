using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class CheckIfIsElementNotInView : ICheck<IElementWrapper>
    {
        private readonly IElementWrapper element;

        public CheckIfIsElementNotInView(IElementWrapper element)
        {
            this.element = element;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsElementInView(element);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is in browser view and should not be. {element.ToString()}");
        }
    }
}