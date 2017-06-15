using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class FastModeWebDriverFactoryMethodWrapper : IFastModeFactory, ISelfCleanUpWebDriver
    {
        private ISelfCleanUpWebDriver factory;

        public FastModeWebDriverFactoryMethodWrapper(Func<ISelfCleanUpWebDriver> factoryMethod)
        {
            this.factory = factoryMethod();
        }

        public IWebDriver CreateNewInstance()
        {
            return factory.Driver;
        }

        public IWebDriver Driver => factory.Driver;
        public void Clear()
        {
            factory.Clear();
        }

        public void Dispose()
        {
            factory.Dispose();
        }

        public void Recreate()
        {
            factory.Recreate();
        }
    }
}