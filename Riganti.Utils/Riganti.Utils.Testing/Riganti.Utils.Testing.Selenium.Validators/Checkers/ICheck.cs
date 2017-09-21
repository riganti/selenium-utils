namespace Riganti.Utils.Testing.Selenium.Validators.Checkers
{
    public interface ICheck<T>
    {
        CheckResult Validate(T wrapper);
    }
}