using Riganti.Utils.Testing.Selenium.Core.Checkers;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public interface IOperationRunner<T>
    {
        void Evaluate<TException>(ICheck<T> check) where TException : TestExceptionBase, new();
    }
}