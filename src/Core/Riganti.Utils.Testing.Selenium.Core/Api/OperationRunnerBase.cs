using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public abstract class OperationRunnerBase<T> : IOperationRunner<T>
    {
        private readonly OperationValidator operationValidator = new OperationValidator();

        protected void EvaluateResult<TException>(CheckResult checkResult)
            where TException : TestExceptionBase, new()
        {
            operationValidator.Validate<TException>(checkResult);
        }

        public abstract void Evaluate<TException>(ICheck<T> validator) where TException : TestExceptionBase, new();
    }
}