using System.Linq;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public class AnyOperationRunner<T> : OperationRunnerBase<T>
    {
        private readonly T[] wrappers;

        public AnyOperationRunner(T[] wrappers)
        {
            this.wrappers = wrappers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TException">Exception based on TestExceptionBase.</typeparam>
        /// <param name="validator">The validator.</param>
        public override void Evaluate<TException>(ICheck<T> validator)
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