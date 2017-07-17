using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfHasAttribute : ICheck
    {
        private readonly string name;

        public CheckIfHasAttribute(string name)
        {
            this.name = name;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.HasAttribute(name);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element has not attribute '{name}'. Element selector: '{wrapper.FullSelector}'.");
        }
    }
}