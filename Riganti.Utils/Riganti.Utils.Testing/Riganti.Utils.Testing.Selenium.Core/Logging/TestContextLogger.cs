using System.Collections.Generic;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core.Logging
{
    /// <summary>
    /// Logger that writes incomming messages into TestContext.
    /// </summary>
    public class TestContextLogger : ILogger
    {
        public string Name => "testContext";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext context, string message, TraceLevel level)
        {
            context.WriteLine(message);
        }

        public void OnTestStarted(ITestContext context)
        {
        }

        public void OnTestFinished(ITestContext context)
        {
        }
    }
}