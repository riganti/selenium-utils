
using System;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{

    /// <summary>
    /// Defines the interface through which the user can log events into some stream.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write message to log.
        /// </summary>
        /// <param name="message">Text to write.</param>
        /// <param name="level">Message information level.</param>
        void WriteLine(string message, TraceLevel level);

        /// <summary>
        /// This method is called when test finished.
        /// </summary>
        void OnTestFinished(ITestContext context);
    }

}