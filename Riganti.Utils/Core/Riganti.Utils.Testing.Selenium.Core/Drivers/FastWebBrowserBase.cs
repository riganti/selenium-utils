using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers
{
    public abstract class FastWebBrowserBase : WebBrowserBase
    {

        public FastWebBrowserBase(IWebBrowserFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Clears the web driver. Use this method to get web browser ready to another test.
        /// </summary>
        public void ClearDriverState()
        {
            try
            {
                ExecuteCleanup();
            }
            catch (Exception ex)
            {
                Factory.LogError(ex);
                throw;
            }
        }

        private void ExecuteCleanup()
        {
            StopWatchedAction(() =>
            {
                if (driverInstance == null) return;

                Factory.LogInfo("Cleaning session");

                ExpectedConditions.AlertIsPresent()(driverInstance)?.Dismiss();
                driverInstance.Manage().Cookies.DeleteAllCookies();
                driverInstance.Manage().Cookies.DeleteCookie(new Cookie("cookie1", "asdasdasd")); //TODO: WTF?

                if (!(driverInstance.Url.Contains("chrome:") || driverInstance.Url.Contains("data:") || driverInstance.Url.Contains("about:")))
                {
                    ((IJavaScriptExecutor)driverInstance).ExecuteScript("if(typeof(Storage) !== undefined) { localStorage.clear(); }");
                    ((IJavaScriptExecutor)driverInstance).ExecuteScript("if(typeof(Storage) !== undefined) { sessionStorage.clear(); }");
                }

                driverInstance.Navigate().GoToUrl("about:blank");
            }, s =>
            {
                Factory.LogInfo($"Session cleaned in {s.ElapsedMilliseconds} ms.");

            });

        }

        /// <summary>
        /// Takes care of disposing the web driver.
        /// </summary>
        public override void KillDriver()
        {
            if (driverInstance != null)
            {
                try
                {
                    try
                    {
                        ExpectedConditions.AlertIsPresent()(driverInstance)?.Dismiss();
                        driverInstance.Dispose();
                    }
                    catch
                    {
                        if (Factory.TestSuiteRunner.Configuration.TestRunOptions.TryToKillWhenNotResponding)
                        {
                            TryToKill(driverInstance);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Factory.LogError(ex);
                }
                finally
                {
                    driverInstance = null;
                }
            }
        }
        /// <summary>
        /// Tries to get PID and kill it.
        /// </summary>
        /// <param name="webDriver">Driver to kill.</param>
        internal void TryToKill(IWebDriver webDriver)
        {
            var commandExecutor = webDriver.GetType()
                .GetProperty("CommandExecutor", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(webDriver) as ICommandExecutor;

            var fields = commandExecutor.GetType().GetRuntimeFields();

            var driverService = fields.FirstOrDefault(s => s.Name == "service")?.GetValue(commandExecutor) as DriverService;
            if (driverService != null)
            {
                var id = driverService.ProcessId;
                KillProcess(id);
                return;
            }

            var commandServer = fields.FirstOrDefault(s => s.Name == "server")?.GetValue(commandExecutor);
            if (commandServer != null)
            {
                var firefoxBinary = commandServer.GetType().GetRuntimeFields().FirstOrDefault(a => a.Name == "process").GetValue(commandServer);
                if (firefoxBinary == null)
                {
                    var firefoxProcess = firefoxBinary.GetType().GetRuntimeFields().FirstOrDefault(a => a.Name == "process").GetValue(commandServer) as Process;
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