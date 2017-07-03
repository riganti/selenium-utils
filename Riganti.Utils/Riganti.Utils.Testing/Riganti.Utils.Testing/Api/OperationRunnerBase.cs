using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public abstract class OperationRunnerBase : IOperationRunner
    {
        private readonly OperationValidator operationValidator = new OperationValidator();

        protected void EvaluateResult(CheckResult checkResult)
        {
            operationValidator.Validate<UnexpectedElementStateException>(checkResult);
        }

        public abstract void Evaluate(ICheck check);
    }
}