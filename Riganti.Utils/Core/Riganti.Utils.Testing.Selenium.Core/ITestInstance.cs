namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestInstance
    {
        ISeleniumTest TestClass { get; }
        TestConfiguration TestConfiguration { get; }
    }
}