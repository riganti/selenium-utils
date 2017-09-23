using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class HasAttributeValidator : ICheck<IElementWrapper>
    {
        private readonly string name;

        public HasAttributeValidator(string name)
        {
            this.name = name;
        }

        public CheckResult Validate(IElementWrapper wrapper)
        {
            var isSucceeded = wrapper.HasAttribute(name);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element has not attribute '{name}'. Element selector: '{wrapper.FullSelector}'.");
        }
    }
}