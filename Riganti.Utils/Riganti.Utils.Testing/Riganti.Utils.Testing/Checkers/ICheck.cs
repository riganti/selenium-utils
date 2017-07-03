namespace Riganti.Utils.Testing.Selenium.Core.Api.Checkers
{
    public interface ICheck
    {
        CheckResult Validate(ElementWrapper wrapper);
    }
}