using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfIsElementNotInView : ICheck
    {
        private readonly ElementWrapper element;

        public CheckIfIsElementNotInView(ElementWrapper element)
        {
            this.element = element;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = !wrapper.IsElementInView(element);
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element is in browser view and should not be. {element.ToString()}");
        }
    }
}