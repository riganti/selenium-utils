using Riganti.Utils.Testing.Selenium.Core.Checkers;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public abstract class OperationRunnerBase<T> : IOperationRunner<T> where T : ISeleniumWrapper
    {
        private readonly OperationValidator operationValidator = new OperationValidator();

        protected void EvaluateResult(CheckResult checkResult)
        {
            operationValidator.Validate<UnexpectedElementStateException>(checkResult);
        }

        public abstract void Evaluate(ICheck<T> check);
    }
}