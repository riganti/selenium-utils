using System.Linq;
using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public class AnyOperationRunner<T> : OperationRunnerBase<T> where T : ISeleniumWrapper
    {
        private readonly T[] wrappers;

        internal AnyOperationRunner(T[] wrappers)
        {
            this.wrappers = wrappers;
        }

        public override void Evaluate(ICheck<T> check)
        {
            var checkResults = wrappers.Select(check.Validate).ToArray();
            var checkResult = CreateCheckResult(checkResults);
            EvaluateResult(checkResult);
        }

        private static CheckResult CreateCheckResult(CheckResult[] checkResults)
        {
            var isSucceeded = checkResults.Any(result => result.IsSucceeded);
            if (isSucceeded)
            {
                return CheckResult.Succeeded;
            }
            return new CheckResult($"The check doesn't match on any element. See {nameof(CheckResult.InnerResults)} for more details", checkResults);
        }
    }
}