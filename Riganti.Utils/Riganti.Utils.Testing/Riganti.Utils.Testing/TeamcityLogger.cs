using System;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TeamcityLogger : ILogger
    {
        public void WriteLine(string message, TraceLevel level)
        {
           Console.WriteLine($"##teamcity[message text='{message}']");
        }
    }
}