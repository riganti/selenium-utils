using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Runtime.Logging
{
    public class TeamcityLogger : ILogger
    {
        public string Name => "teamcity";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext context, string message, TraceLevel level)
        {
           Console.WriteLine($"##teamcity[message text='{message}']");
        }

        public void OnTestStarted(ITestContext context)
        {
        }

        public void OnTestFinished(ITestContext context)
        {
        }
    }
}