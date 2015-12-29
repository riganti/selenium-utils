using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            return new ChromeDriver(options);
        }
    }
}