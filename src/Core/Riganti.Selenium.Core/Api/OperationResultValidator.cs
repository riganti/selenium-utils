using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Validators.Checkers;

namespace Riganti.Selenium.Core.Api
{
    public class OperationResultValidator
    {
        public void Validate<TException>(CheckResult checkResult)
            where TException : TestExceptionBase, new()
        {
            if (!checkResult.IsSucceeded)
            {
                throw new TException() { CheckResult = checkResult };
            }
        }
    }
}