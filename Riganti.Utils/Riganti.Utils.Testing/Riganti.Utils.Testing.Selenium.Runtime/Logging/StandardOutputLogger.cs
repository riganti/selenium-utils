using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Runtime.Logging
{
    /// <summary>
    /// Logger that writes by Console.WriteLine
    /// </summary>
    public class StandardOutputLogger : ILogger
    {
        public string Name => "Stdout";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext context, string message,  TraceLevel level)
        {
            Console.WriteLine(message);
        }

        public void OnTestStarted(ITestContext context)
        {
        }

        public void OnTestFinished(ITestContext context)
        {
        }
    }
    
}