using System;
using System.Collections.Generic;
using System.Diagnostics;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Logging
{
    public class TeamcityLogger : ILogger
    {
        public string Name => "teamcity";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
           Console.WriteLine($"##teamcity[message text='{message}']");
        }

        public void OnTestStarted(ITestContext instanceContext)
        {
        }

        public void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}