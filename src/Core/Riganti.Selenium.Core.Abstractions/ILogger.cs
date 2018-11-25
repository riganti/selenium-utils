using System.Collections.Generic;
using System.Diagnostics;

namespace Riganti.Selenium.Core.Abstractions
{
    /// <summary>
    /// Defines the interface through which the user can log events into some stream.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets the configured name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the configured logger options.
        /// </summary>
        IDictionary<string, string> Options { get; }

        /// <summary>
        /// Write message to log.
        /// </summary>
        /// <param name="message">Text to write.</param>
        /// <param name="level">Message information level.</param>
        void WriteLine(ITestContext instanceContext, string message, TraceLevel level);

        /// <summary>
        /// This method is called when test started.
        /// </summary>
        void OnTestStarted(ITestContext instanceContext);

        /// <summary>
        /// This method is called when test finished.
        /// </summary>
        void OnTestFinished(ITestContext instanceContext);
    }
}