using System;
using System.Diagnostics;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.DotVVM
{
    public static class DotVVMBrowserWrapperExtensions
    {
        /// <summary>
        /// Determines whether tested page is dotvvm.
        /// The detection is ensured by waiting for invocation of DotVVM Init.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="maxDotvvmLoadTimeout">is the maximum time interval in which the DotVVM has to be loaded.</param>
        public static bool IsDotvvmPage(this IBrowserWrapper browser, long maxDotvvmLoadTimeout = 8000)
        {
            try
            {
                var result = "loading";
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var isFirstRun = true;
                while (stopwatch.ElapsedMilliseconds < maxDotvvmLoadTimeout && result == "loading")
                {
                    if (!isFirstRun)
                    {
                        browser.Wait(30);
                    }
                    isFirstRun = false;

                    var executor = browser.GetJavaScriptExecutor();
                    result = executor.ExecuteScript(
       @"return (function seleniumUtils_IsDotvvmPageLoaded() {
            var state = (window.__riganti_selenium_utils_dotvvm || (window.__riganti_selenium_utils_dotvvm = {}));
            if (state.inited) {
                return true;
            }
            if (window.dotvvm != null) {
                dotvvm.events.init.subscribe(function () { state.inited = true });
                state.loaded = true;
            }
            if (!state.loaded && document.scripts.length > 0) {
                for (var key in document.scripts) {
                    if (document.scripts.hasOwnProperty(key)) {
                        var script = document.scripts[key];
                        if (script.src.indexOf('/dotvvm--internal') > -1) {
                            state.loaded = true;
                            return 'loading';
                        } else {
                            if (document.readyState === 'complete') {
                                return false;
                            }
                        }
                    }
                }
            }

            if (!state.loaded && document.readyState !== 'complete') {
                state.loaded = false;
                return 'loading';
            }
            return state.inited || 'loading';
        })();"
                                    )?.ToString();
                }

                stopwatch.Stop();
                if (result == "loading")
                {
                    return false;
                }
                else
                {
                    return string.Equals(result, "true", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits until DotVVM event Init is performed.
        ///</summary>
        /// <param name="browser"></param>
        /// <param name="maxDotvvmLoadTimeout">is the maximum time interval in which the DotVVM has to be loaded.</param>
        public static void WaitUntilDotvvmInited(this IBrowserWrapper browser, int maxDotvvmLoadTimeout = 8000)
        {
            if (!IsDotvvmPage(browser, maxDotvvmLoadTimeout))
            {
                throw new PageLoadException("Page did not initiate DotVVM.");
            }
        }

        /// <summary>
        /// Waits for DotVVM postback to be finished.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeout">Timeout in ms.</param>
        public static void WaitForPostback(this IBrowserWrapper browser, int timeout = 20000)
        {
            if (browser.IsDotvvmPage())
            {
                browser.WaitFor(() =>
                    {
                        var result = browser.GetJavaScriptExecutor().ExecuteScript("return dotvvm.isPostbackRunning()").ToString();
                        return string.Equals("false", result, StringComparison.OrdinalIgnoreCase);
                    }
                , timeout, "DotVVM postback still running.");
            }
        }
    }
}