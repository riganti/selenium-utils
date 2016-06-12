using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface IWebDriverFactory
    {

        IWebDriver CreateNewInstance();
    }
}