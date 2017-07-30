using System.Collections.Generic;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Runtime.Logging
{
    public class DebugLogger : ILogger
    {
        public string Name => "debug";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext context, string message, TraceLevel level)
        {
            Debug.WriteLine(message, "Test");
            Debug.Flush();
        }

        public void OnTestStarted(ITestContext context)
        {
        }

        public void OnTestFinished(ITestContext context)
        {
        }
    }
}