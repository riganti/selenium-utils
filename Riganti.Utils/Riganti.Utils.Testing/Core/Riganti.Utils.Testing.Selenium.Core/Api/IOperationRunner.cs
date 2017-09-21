using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using Riganti.Utils.Testing.Selenium.Validators.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public interface IOperationRunner<T>
    {
        void Evaluate<TException>(ICheck<T> check) where TException : TestExceptionBase, new();
    }
}