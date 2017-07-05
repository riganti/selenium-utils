using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfDoesNotContainText : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = !string.IsNullOrWhiteSpace(wrapper.GetInnerText());
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element does contain text. Element should be empty.\r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}