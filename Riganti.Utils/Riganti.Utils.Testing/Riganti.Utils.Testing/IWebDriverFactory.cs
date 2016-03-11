using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface IWebDriverFactory
    {

        IWebDriver CreateNewInstance();
    }
}