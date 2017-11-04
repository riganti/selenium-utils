using System.Collections.Generic;
using System.Diagnostics;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core.Logging
{
    /// <summary>
    /// Logger that writes incomming messages into TestContext.
    /// </summary>
    public class TestContextLogger : ILogger
    {
        public string Name => "testContext";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            instanceContext.WriteLine(message);
        }

        public void OnTestStarted(ITestContext instanceContext)
        {
        }

        public void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}