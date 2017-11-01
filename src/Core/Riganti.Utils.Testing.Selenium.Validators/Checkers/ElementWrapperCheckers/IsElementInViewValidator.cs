using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class IsElementInViewValidator : ICheck<IElementWrapper>
    {
        private readonly IElementWrapper element;

        public IsElementInViewValidator(IElementWrapper element)
        {
            this.element = element;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsElementInView(element);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not in browser view. {element.ToString()}");
        }
    }
}