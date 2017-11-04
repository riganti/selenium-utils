namespace Riganti.Selenium.Core
{
    public interface ITestInstance
    {
        ISeleniumTest TestClass { get; }
        TestConfiguration TestConfiguration { get; }
    }
}