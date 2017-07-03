using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Api
{
    public interface IOperationRunner
    {
        void Evaluate(ICheck check);
    }
}