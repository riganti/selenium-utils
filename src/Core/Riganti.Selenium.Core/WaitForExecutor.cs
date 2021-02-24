using OpenQA.Selenium;
using System;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using System.Threading;
using System.Runtime;

namespace Riganti.Selenium.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class WaitForExecutor
    {
        public void WaitFor(Action condition, int timeout, string failureMessage, int checkInterval, bool throwOriginal)
        {
            if (condition == null)
            {
                throw new NullReferenceException("Condition cannot be null.");
            }
            var now = DateTime.UtcNow;

            bool isConditionMet = false;
            do
            {
                try
                {
                    condition();
                    isConditionMet = true;
                }
                catch (StaleElementReferenceException)
                {
                    if (DateTime.UtcNow.Subtract(now).TotalMilliseconds > timeout)
                    {
                        if (throwOriginal) throw;
                        else
                            throw new WaitBlockException(failureMessage);
                    }
                }
                catch (InvalidElementStateException)
                {
                    if (DateTime.UtcNow.Subtract(now).TotalMilliseconds > timeout)
                    {
                        if (throwOriginal) throw;
                        else throw new WaitBlockException(failureMessage);
                    }
                }
                Thread.Sleep(checkInterval);
            } while (!isConditionMet);
        }

        /// <summary>
        /// Waits until condition does not throw exception.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="options">Condition that determine maximum timeout, check interval etc.. Default value is set to <see cref="WaitForOptions.DefaultOptions"/>.</param>
        public static void WaitFor(Action condition, WaitForOptions options = null)
        {
            if (condition is null) throw new ArgumentNullException(nameof(condition));
            if (options == null) options = WaitForOptions.DefaultOptions;

            if (options.Enabled)
            {
                var executor = new WaitForExecutor();
                executor.WaitFor(condition, options.Timeout, options.FailureMessage, options.CheckInterval, options.ThrowOriginalException);
            }
            else
            {
                condition();
            }
        }
    }
}
