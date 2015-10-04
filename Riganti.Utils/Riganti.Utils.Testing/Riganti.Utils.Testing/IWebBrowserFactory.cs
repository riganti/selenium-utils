using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface IWebDriverFactory
    {
        IWebDriver CreateNewInstance();
    }

    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            return new ChromeDriver(options);
        }
    }

    public class DefaultInternetExplorerWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            return new InternetExplorerDriver();
        }
    }

    public class DefaultFirefoxWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            return new FirefoxDriver();
        }
    }

    public class WebDriverFactoryMethodWrapper : IWebDriverFactory
    {
        private readonly Func<IWebDriver> factoryMethod;

        public WebDriverFactoryMethodWrapper(Func<IWebDriver> factoryMethod)
        {
            this.factoryMethod = factoryMethod;
        }

        public IWebDriver CreateNewInstance()
        {
            return factoryMethod();
        }
    }
}