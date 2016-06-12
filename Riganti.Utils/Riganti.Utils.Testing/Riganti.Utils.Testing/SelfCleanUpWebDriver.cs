using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public abstract class SelfCleanUpWebDriver : IReusableWebDriver, ISelfCleanUpWebDriver
    {
        private IWebDriver driver;

        public IWebDriver Driver
        {
            get
            {
                if (driver != null) return driver;
                driver = CreateInstance();
                return driver;
            }
        }

        protected abstract IWebDriver CreateInstance();

        public virtual void Clear()
        {
            SeleniumTestBase.Log("Cleaning session");
            ExpectedConditions.AlertIsPresent()(Driver)?.Dismiss();
            Driver.Manage().Cookies.DeleteAllCookies();

            if (!(Driver.Url.Contains("chrome:") || Driver.Url.Contains("data:") || Driver.Url.Contains("about:")))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "if(typeof(Storage) !== undefined) { localStorage.clear(); }");

                ((IJavaScriptExecutor)driver).ExecuteScript(
                    "if(typeof(Storage) !== undefined) { sessionStorage.clear(); }");
            }

            Driver.Navigate().GoToUrl("about:blank");
        }

        public void Recreate()
        {
            SeleniumTestBase.LogDriverId(driver, "Recreation - SelfCleanUpWebDriver (OLD)");
            Dispose();
            driver = CreateInstance();
            SeleniumTestBase.LogDriverId(driver, "Recreation - SelfCleanUpWebDriver (NEW)");
        }

        public void Dispose()
        {
            if (driver != null)
            {
                try
                {
                  SeleniumTestBase.LogDriverId(driver, "Dispose    - SelfCleanUpWebDriver");
                    ExpectedConditions.AlertIsPresent()(Driver)?.Dismiss();
                    driver.Close();
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}