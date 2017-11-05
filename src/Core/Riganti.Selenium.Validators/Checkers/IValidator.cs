using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers
{
    public interface IValidator<in T> where T : ISupportedByValidator
    {
        CheckResult Validate(T wrapper);
    }
}