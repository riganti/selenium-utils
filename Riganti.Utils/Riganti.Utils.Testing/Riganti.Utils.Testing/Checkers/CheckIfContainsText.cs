using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public class CheckIfContainsText : ICheck
    {
        public CheckResult Validate(ElementWrapper wrapper)
        {
            var isSucceeded = string.IsNullOrWhiteSpace(wrapper.GetInnerText());
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Element doesn't contain text. \r\n Element selector: {wrapper.Selector} \r\n");
        }
    }
}