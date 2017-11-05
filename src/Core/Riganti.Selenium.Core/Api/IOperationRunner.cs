using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Validators.Checkers;

namespace Riganti.Selenium.Core.Api
{
    public interface IOperationRunner<T> where T : ISupportedByValidator
    {
        void Evaluate<TException>(IValidator<T> validator) where TException : TestExceptionBase, new();
    }
}