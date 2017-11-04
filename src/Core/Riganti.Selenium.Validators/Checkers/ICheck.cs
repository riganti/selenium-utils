namespace Riganti.Selenium.Validators.Checkers
{
    public interface ICheck<T>
    {
        CheckResult Validate(T wrapper);
    }
}