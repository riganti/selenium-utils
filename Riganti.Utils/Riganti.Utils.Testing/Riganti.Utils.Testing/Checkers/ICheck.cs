namespace Riganti.Utils.Testing.Selenium.Core.Checkers
{
    public interface ICheck<T> where T : ISeleniumWrapper
    {
        CheckResult Validate(T wrapper);
    }
}