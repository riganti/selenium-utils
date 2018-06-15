using System.Collections.Generic;
using System.Diagnostics;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core.Logging
{
    /// <summary>
    /// Logger that writes into Diagnostics.Trace
    /// </summary>
    public class TraceLogger : ILogger
    {
        public virtual string Name => "trace";

        public  IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Write message to log.
        /// </summary>
        /// <param name="message">Text to write.</param>
        /// <param name="level">message importance</param>
        public virtual void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            switch (level)
            {
                case TraceLevel.Error:
                    Trace.TraceError(message);
                    break;

                case TraceLevel.Info:
                case TraceLevel.Verbose:
                    Trace.TraceInformation(message);
                    break;

                case TraceLevel.Warning:
                    Trace.TraceWarning(message);
                    break;

                default:
                    Trace.WriteLine(message, "SeleniumTest");
                    break;
            }
        }

        public virtual void OnTestStarted(ITestContext instanceContext)
        {
        }

        public virtual void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}