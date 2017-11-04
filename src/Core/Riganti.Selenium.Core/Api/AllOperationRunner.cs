using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Selenium.Validators.Checkers;

namespace Riganti.Selenium.Core.Api
{
    public class AllOperationRunner<T> : OperationRunnerBase<T>
    {
        private readonly IEnumerable<T> wrappers;

        public AllOperationRunner(IEnumerable<T> wrappers)
        {
            this.wrappers = wrappers;
        }

        public override void Evaluate<TException>(ICheck<T> validator)
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
