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
        public void OnTestFinished(ITestContext context)
        {
            //ignore
        }
    }
}