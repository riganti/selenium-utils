using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class IsElementNotInViewValidator : IValidator<IElementWrapper>
    {
        private readonly IElementWrapper element;

        public IsElementNotInViewValidator(IElementWrapper element)
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