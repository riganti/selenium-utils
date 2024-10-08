﻿using System;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using System.Threading;
using System.Diagnostics;

namespace Riganti.Selenium.Core
{
    public class WaitForExecutor
    {
        public void WaitFor(Action condition, int timeout, string failureMessage, int checkInterval, bool throwOriginal)
        {
            if (condition == null)
            {
                throw new NullReferenceException("Condition cannot be null.");
            }
            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                try
                {
                    condition();
                    return;
                }
                catch (TestExceptionBase ex)
                {
                    if (stopwatch.ElapsedMilliseconds > timeout)
                    {
                        if (throwOriginal) throw;
                        else throw new WaitBlockException(failureMessage ?? ex.Message, ex);
                    }
                }
                Thread.Sleep(checkInterval);
            }
        }
        public void WaitFor(Func<bool> condition, WaitForOptions options = null)
        {
            options ??= WaitForOptions.DefaultOptions;
            if (condition == null)
            {
                throw new NullReferenceException("Condition cannot be null.");
            }
            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                try
                {
                    if (condition())
                        return;
                }
                catch (TestExceptionBase ex)
                {
                    if (stopwatch.ElapsedMilliseconds > options.Timeout)
                    {
                        if (options.ThrowOriginalException) throw;
                        else throw new WaitBlockException(options.FailureMessage ?? ex.Message, ex);
                    }
                }
                if (stopwatch.ElapsedMilliseconds > options.Timeout)
                {
                    throw new WaitBlockException(options.FailureMessage ?? "Condition returned false after timeout expired.");
                }

                Thread.Sleep(options.CheckInterval);
            }
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
