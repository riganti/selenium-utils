using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface IWebDriverFactory
    {
        /// <summary>
        /// Creates new instance of the browser.
        /// </summary>
        IWebDriver CreateNewInstance();
    }
}