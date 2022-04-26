using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers
{
    /// <inheritdoc />
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

                DismissAllAlerts();
                DeleteAllCookies();

                CleanSessionAndLocalStorage();

                driverInstance.Navigate().GoToUrl("about:blank");
            }, s =>
            {
                Factory.LogInfo($"Session cleaned in {s.ElapsedMilliseconds} ms.");

            });

        }

        protected virtual void CleanSessionAndLocalStorage()
        {
            if (!(driverInstance.Url.Contains("chrome:") || driverInstance.Url.Contains("data:") || driverInstance.Url.Contains("about:")))
            {
                ((IJavaScriptExecutor)driverInstance).ExecuteScript("if(typeof(Storage) !== undefined) { localStorage.clear(); }");
                ((IJavaScriptExecutor)driverInstance).ExecuteScript("if(typeof(Storage) !== undefined) { sessionStorage.clear(); }");
            }
        }

        /// <summary>
        /// Removed all cookies from browser during cleaning session
        /// </summary>
        protected virtual void DeleteAllCookies()
        {
            driverInstance.Manage().Cookies.DeleteAllCookies();
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
                        DismissAllAlerts();
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
        /// Dismisses all alerts during cleaning session
        /// </summary>
        protected virtual void DismissAllAlerts()
        {
            driverInstance.SwitchTo()?.Alert()?.Dismiss();
        }

        /// <summary>
        /// Tries to get PID and kill it.
        /// </summary>
        /// <param name="webDriver">Driver to kill.</param>
        internal void TryToKill(IWebDriver webDriver)
        {
            var driverWithExecutor = webDriver as IHasCommandExecutor;
            if (driverWithExecutor is null) return;

            driverWithExecutor.CommandExecutor.Dispose();
            var s= Process.Start("");
        }
    }
}