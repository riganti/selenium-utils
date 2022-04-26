using System.Collections.Generic;
using System.Linq;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Validators.Checkers;

namespace Riganti.Selenium.Core.Api
{
    public class AllOperationRunner<T> : OperationRunnerBase<T> where T : ISupportedByValidator
    {
        private readonly IEnumerable<T> wrappers;
        private readonly WaitForOptions waitForOptions;

        public AllOperationRunner(IEnumerable<T> wrappers, WaitForOptions waitForOptions)
        {
            this.wrappers = wrappers;
            this.waitForOptions = waitForOptions;
        }

        public override void Evaluate<TException>(IValidator<T> validator)
        {
            var checkResults = wrappers.Select(validator.Validate).ToArray();
            var checkResult = CreateCheckResult(checkResults);
            EvaluateResult<TException>(checkResult);
        }

        private static CheckResult CreateCheckResult(CheckResult[] checkResults)
        {
            var isSucceeded = checkResults.All(result => result.IsSucceeded);
            if (isSucceeded)
            {
                return CheckResult.Succeeded;
            }
            return new CheckResult(
                $"The validator doesn't match on all elements. See {nameof(CheckResult.InnerResults)} for more details",
                checkResults);
        }
    }
}
