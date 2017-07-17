using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public interface IOperationRunner<T> where T : ISeleniumWrapper
    {
        void Evaluate(ICheck<T> check);
    }
}