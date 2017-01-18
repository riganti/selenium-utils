using System;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Logger that writes into Diagnostics.Trace
    /// </summary>
    public class TraceLogger : ILogger
    {
        /// <summary>
        /// Write message to log.
        /// </summary>
        /// <param name="message">Text to write.</param>
        /// <param name="level">message importance</param>
        public void WriteLine(string message, TraceLevel level)
        {
            Trace.WriteLine(message, "SeleniumTest");
        }
    }
}