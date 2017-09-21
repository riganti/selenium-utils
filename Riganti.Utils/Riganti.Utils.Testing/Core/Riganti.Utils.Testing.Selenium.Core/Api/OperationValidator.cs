using System;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public class OperationValidator
    {
        public void Validate<TException>(CheckResult checkResult)
            where TException : TestExceptionBase, new()
        {
            if (!checkResult.IsSucceeded)
            {
                throw new TException() {CheckResult = checkResult};
            }
        }
    }
}