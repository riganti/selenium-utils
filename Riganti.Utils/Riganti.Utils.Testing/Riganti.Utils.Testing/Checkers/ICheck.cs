namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public interface ICheck<T>
    {
        CheckResult Validate(T wrapper);
    }
}