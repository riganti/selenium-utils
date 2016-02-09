using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public abstract class SelfCleanUpWebDriver : IReusableWebDriver, ISelfCleanUpWebDriver
    {
        private IWebDriver driver;

        public IWebDriver Driver
        {
            get
            {
                if (driver != null) return driver;
                return driver = CreateInstance();
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
            SeleniumTestBase.Log("Recreating driver.");
            Dispose();
            driver = CreateInstance();
            Driver.Navigate().GoToUrl("about:blank");
        }


        public void Dispose()
        {
            if (driver != null)
            {
                SeleniumTestBase.Log("Closing driver.");
                ExpectedConditions.AlertIsPresent()(Driver)?.Dismiss();
                driver.Close();
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}