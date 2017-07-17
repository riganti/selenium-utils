using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public class AllOperationRunner : OperationRunnerBase
    {
        private readonly ElementWrapper[] wrappers;

        internal AllOperationRunner(ElementWrapper[] wrappers)
        {
            this.wrappers = wrappers;
        }

        public override void Evaluate(ICheck check)
        {
            var checkResults = wrappers.Select(check.Validate).ToArray();
            var checkResult = CreateCheckResult(checkResults);
            EvaluateResult(checkResult);
        }

        private static CheckResult CreateCheckResult(CheckResult[] checkResults)
        {
            var isSucceeded = checkResults.All(result => result.IsSucceeded);
            if (isSucceeded)
            {
                return CheckResult.Succeeded;
            }
            return new CheckResult(
                $"The check doesn't match on all elements. See {nameof(CheckResult.InnerResults)} for more details",
                checkResults);
        }
    }
}
