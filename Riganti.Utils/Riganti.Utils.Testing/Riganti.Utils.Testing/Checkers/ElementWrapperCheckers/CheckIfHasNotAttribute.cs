using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfHasNotAttribute : ICheck
    {
        private readonly string name;

        public CheckIfHasNotAttribute(string name)
        {
            this.name = name;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.HasAttribute(name);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element has not attribute '{name}'. Element selector: '{wrapper.FullSelector}'.");
        }
    }
}