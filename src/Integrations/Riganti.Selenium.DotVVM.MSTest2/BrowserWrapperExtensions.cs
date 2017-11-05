using System;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core;

namespace Riganti.Selenium.DotVVM
{
    public static class BrowserWrapperExtensions
    {
        /// <summary>
        /// Determines whether tested page is dotvvm.
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static bool IsDotvvmPage(this IBrowserWrapper browser)
        {
            try
            {
                return string.Equals("true",
                    browser.GetJavaScriptExecutor().ExecuteScript("return dotvvm instanceof DotVVM").ToString(),
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits for dotvvm postback to be finished.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeout">Timeout in ms.</param>
        public static void WaitForPostback(this IBrowserWrapper browser, int timeout = 20000)
        {
            browser.WaitFor(() => string.Equals("true", browser.GetJavaScriptExecutor().ExecuteScript("return dotvvm.isPostbackRunning()").ToString(), StringComparison.OrdinalIgnoreCase), timeout, "DotVVM postback still running.");
        }

    }
}