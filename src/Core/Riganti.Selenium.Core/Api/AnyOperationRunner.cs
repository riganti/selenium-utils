using System.Collections.Generic;
using System.Linq;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Validators.Checkers;

namespace Riganti.Selenium.Core.Api
{
    public class AnyOperationRunner<T> : OperationRunnerBase<T> where T : ISupportedByValidator
    {
        private readonly IEnumerable<T> wrappers;

        public AnyOperationRunner(IEnumerable<T> wrappers)
        {
            this.wrappers = wrappers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TException">Exception based on TestExceptionBase.</typeparam>
        /// <param name="validator">The validator.</param>
        public override void Evaluate<TException>(IValidator<T> validator)
        {
            var checkResults = wrappers.Select(validator.Validate).ToArray();
            var checkResult = CreateCheckResult(checkResults);
            EvaluateResult<TException>(checkResult);
        }

        private static CheckResult CreateCheckResult(CheckResult[] checkResults)
        {
            var isSucceeded = checkResults.Any(result => result.IsSucceeded);
            if (isSucceeded)
            {
                return CheckResult.Succeeded;
            }
            return new CheckResult($"The validator doesn't match on any element. See {nameof(CheckResult.InnerResults)} for more details:", checkResults);
        }
    }
}