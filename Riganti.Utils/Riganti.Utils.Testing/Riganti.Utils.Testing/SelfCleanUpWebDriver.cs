using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// This wrapper cares of recreating and cleaning of web driver.
    /// </summary>
    public abstract class SelfCleanUpWebDriver : IReusableWebDriver, ISelfCleanUpWebDriver
    {
        private IWebDriver driver;
        /// <summary>
        /// Represents browser .NET Bindings (SeleniumHQ).
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                if (driver != null) return driver;
                driver = CreateInstance();
                return driver;
            }
        }

        /// <summary>
        /// Creates new instance of web driver.
        /// </summary>
        /// <returns></returns>
        protected abstract IWebDriver CreateInstance();

        /// <summary>
        /// Clears the web driver. Use this method to get web browser ready to another test.
        /// </summary>
        public virtual void Clear()
        {
            try
            {
                ExecuteCleanup();
            }
            catch (Exception ex)
            {
                SeleniumTestBase.Log(ex.ToString());
                SeleniumTestBase.Log("Browser cleaning failed! Recreating the browser.");
                Recreate();
                SeleniumTestBase.Log("New browser instance created.");
            }
        }

        private void ExecuteCleanup()
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

        /// <summary>
        /// Disposes and recreates the web driver.
        /// </summary>
        public void Recreate()
        {
            SeleniumTestBase.LogDriverId(driver, "Recreation - SelfCleanUpWebDriver (OLD)");
            Dispose();
            driver = CreateInstance();
            SeleniumTestBase.LogDriverId(driver, "Recreation - SelfCleanUpWebDriver (NEW)");
        }

        /// <summary>
        /// Takes care of disposing the web driver.
        /// </summary>
        public void Dispose()
        {
            if (driver != null)
            {
                try
                {
                    try
                    {
                        SeleniumTestBase.LogDriverId(driver, "Dispose - SelfCleanUpWebDriver");
                        ExpectedConditions.AlertIsPresent()(Driver)?.Dismiss();
                        driver.Dispose();
                    }
                    catch
                    {
                        if (SeleniumTestsConfiguration.TryToKillWhenNotResponding)
                        {
                            TryToKill(driver);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SeleniumTestBase.Log(ex.ToString(), 10);
                    //ignore
                }
                finally
                {
                    driver = null;
                }
            }
        }
        /// <summary>
        /// Tries to get PID and kill it.
        /// </summary>
        /// <param name="driver">Driver to kill.</param>
        internal void TryToKill(IWebDriver driver)
        {
            var commandExecutor = driver.GetType()
                .GetProperty("CommandExecutor", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(driver) as ICommandExecutor;

            var fields = commandExecutor.GetType().GetRuntimeFields();

            var driverService = fields.FirstOrDefault(s => s.Name == "service")?.GetValue(commandExecutor) as DriverService;
            if (driverService != null)
            {
                var id = driverService.ProcessId;
                KillProcess(id);
                return;
            }
            var commandServer =
                fields.FirstOrDefault(s => s.Name == "server")?.GetValue(commandExecutor);
            if (commandServer != null)
            {
                var firefoxBinary = commandServer
                    .GetType().GetRuntimeFields().FirstOrDefault(a => a.Name == "process").GetValue(commandServer);
                if (firefoxBinary == null)
                {
                    var firefoxProcess = firefoxBinary
                    .GetType().GetRuntimeFields().FirstOrDefault(a => a.Name == "process").GetValue(commandServer) as Process;
                    KillProcess(firefoxProcess.Id);
                }
            }
        }

        /// <summary>
        /// Kills the process.
        /// </summary>
        /// <param name="id"></param>
        private void KillProcess(int id)
        {
            var process = Process.GetProcessById(id);
            if (!process.CloseMainWindow())
            {
                process.Close();
            }
        }
    }
}