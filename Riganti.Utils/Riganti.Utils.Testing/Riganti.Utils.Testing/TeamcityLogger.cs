using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TeamcityLogger : ILogger
    {
        public void WriteLine(string message)
        {
           Console.WriteLine($"##teamcity[message text='{message}']");
        }
    }
}