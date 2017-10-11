using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public interface IOperationRunner<T>
    {
        void Evaluate<TException>(ICheck<T> validator) where TException : TestExceptionBase, new();
    }
}