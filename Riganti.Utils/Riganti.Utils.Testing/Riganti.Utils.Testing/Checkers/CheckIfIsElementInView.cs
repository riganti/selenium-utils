using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsElementInView : ICheck
    {
        private readonly ElementWrapper element;

        public CheckIfIsElementInView(ElementWrapper element)
        {
            this.element = element;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = wrapper.IsElementInView(element);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is not in browser view. {element.ToString()}");
        }
    }
}