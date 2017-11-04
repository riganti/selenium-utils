using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Validators.Checkers;

namespace Riganti.Selenium.Core.Api
{
    public interface IOperationRunner<T>
    {
        void Evaluate<TException>(ICheck<T> validator) where TException : TestExceptionBase, new();
    }
}