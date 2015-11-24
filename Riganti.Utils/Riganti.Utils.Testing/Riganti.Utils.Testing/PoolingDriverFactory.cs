using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.Concurrent;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class PoolingDriverFactory : IWebDriverFactory
    {
        public IWebDriverFactory Factory { get; }
        public TimeSpan DropTimeout { get; }
        private ConcurrentStack<IWebDriverToken> pool = new ConcurrentStack<IWebDriverToken>();
        private Stopwatch timeoutWatch = Stopwatch.StartNew();

        public PoolingDriverFactory(IWebDriverFactory factory)
        {
            Factory = factory;
        }

        private void ReturnDriver(IWebDriverToken driver)
        {
            pool.Push(driver);
        }

        public IWebDriverToken CreateNewInstance()
        {
            IWebDriverToken driver;
            if (!pool.TryPop(out driver))
            {
                timeoutWatch.Restart();
                // create new driver
                driver = Factory.CreateNewInstance();
            }
            if (pool.Count == 0)
            {
                timeoutWatch.Restart();
            }
            return new PoolDriverToken(this, driver);
        }

        ~PoolingDriverFactory()
        {
            var p = pool;
            pool = new ConcurrentStack<IWebDriverToken>();
            foreach (var driver in p)
            {
                driver.Dispose();
            }
        }

        class PoolDriverToken : IWebDriverToken
        {
            public IWebDriver Driver => driverToken.Driver;
            private IWebDriverToken driverToken;
            PoolingDriverFactory pool;

            public PoolDriverToken(PoolingDriverFactory pool, IWebDriverToken driver)
            {
                driverToken = driver;
                this.pool = pool;
            }

            public void Dispose()
            {
                try
                {
                    ExpectedConditions.AlertIsPresent()(Driver)?.Dismiss();
                    Driver.Manage().Cookies.DeleteAllCookies();
                    Driver.Navigate().GoToUrl("about:blank");
                    pool.ReturnDriver(driverToken);
                    pool = null;
                }
                catch
                {
                    // ignore, recyclation failed - we'll create new one
                }
            }
        }
    }
}
