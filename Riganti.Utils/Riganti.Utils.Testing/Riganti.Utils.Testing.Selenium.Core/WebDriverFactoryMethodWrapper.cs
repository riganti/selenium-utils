using System;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core
{
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