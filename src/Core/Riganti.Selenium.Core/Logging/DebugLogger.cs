using System.Collections.Generic;
using System.Diagnostics;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core.Logging
{
    public class DebugLogger : ILogger
    {
        public string Name => "debug";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            Debug.WriteLine(message, "Test");
            Debug.Flush();
        }

        public void OnTestStarted(ITestContext instanceContext)
        {
        }

        public void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}