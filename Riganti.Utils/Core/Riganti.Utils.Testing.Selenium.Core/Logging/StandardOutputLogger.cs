using System;
using System.Collections.Generic;
using System.Diagnostics;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Logging
{
    /// <summary>
    /// Logger that writes by Console.WriteLine
    /// </summary>
    public class StandardOutputLogger : ILogger
    {
        public string Name => "Stdout";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext instanceContext, string message,  TraceLevel level)
        {
            Console.WriteLine(message);
        }

        public void OnTestStarted(ITestContext instanceContext)
        {
        }

        public void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
    
}