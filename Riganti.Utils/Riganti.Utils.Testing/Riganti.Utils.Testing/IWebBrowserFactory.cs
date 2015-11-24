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
        IWebDriverToken CreateNewInstance();
    }

    public interface IWebDriverToken: IDisposable
    {
        IWebDriver Driver { get; }
    }

    class SimpleWebDriverToken : IWebDriverToken
    {
        public IWebDriver Driver { get; }

        public SimpleWebDriverToken(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }

    public class DefaultChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriverToken CreateNewInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            return new SimpleWebDriverToken(new ChromeDriver(options));
        }
    }

    public class DefaultInternetExplorerWebDriverFactory : IWebDriverFactory
    {
        public IWebDriverToken CreateNewInstance()
        {
            return new SimpleWebDriverToken(new InternetExplorerDriver());
        }
    }

    public class DefaultFirefoxWebDriverFactory : IWebDriverFactory
    {
        public IWebDriverToken CreateNewInstance()
        {
            return new SimpleWebDriverToken(new FirefoxDriver());
        }
    }

    public class WebDriverFactoryMethodWrapper : IWebDriverFactory
    {
        private readonly Func<IWebDriver> factoryMethod;

        public WebDriverFactoryMethodWrapper(Func<IWebDriver> factoryMethod)
        {
            this.factoryMethod = factoryMethod;
        }

        public IWebDriverToken CreateNewInstance()
        {
            return new SimpleWebDriverToken(factoryMethod());
        }
    }
}