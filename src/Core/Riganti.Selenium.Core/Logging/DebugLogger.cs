using System.Collections.Generic;
using System.Diagnostics;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core.Logging
{
    public class DebugLogger : ILogger
    {
        public virtual string Name => "debug";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public virtual void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            Debug.WriteLine(message, "Test");
            Debug.Flush();
        }

        public virtual void OnTestStarted(ITestContext instanceContext)
        {
        }

        public virtual void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}