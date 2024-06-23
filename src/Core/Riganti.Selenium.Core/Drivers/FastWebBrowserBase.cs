using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
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
         
            // Firefox driver doesn't list all cookies in Manage().Cookies - therefore JS invocation to remove all cookies
            // Copied from https://stackoverflow.com/a/33366171
            driverInstance.ExecuteJavaScript(
                @"
(function () {
    var cookies = document.cookie.split(""; "");
    for (var c = 0; c < cookies.length; c++) {
        var d = window.location.hostname.split(""."");
        while (d.length > 0) {
            var cookieBase = encodeURIComponent(cookies[c].split("";"")[0].split(""="")[0]) + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT; domain=' + d.join('.') + ' ;path=';
            var p = location.pathname.split('/');
            document.cookie = cookieBase + '/';
            while (p.length > 0) {
                document.cookie = cookieBase + p.join('/');
                p.pop();
            };
            d.shift();
        }
    }
})();
"
                );
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
                        Factory.LogVerbose($"Dismissing alerts in and then killing browser '{UniqueName}'.");
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
            try
            {
                driverInstance.SwitchTo()?.Alert()?.Dismiss();
            }
            catch (NoAlertPresentException)
            {
                // ignore it
            }
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
            var s = Process.Start("");
        }
    }
}
