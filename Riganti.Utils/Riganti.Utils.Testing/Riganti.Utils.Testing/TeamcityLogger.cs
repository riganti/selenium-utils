using System;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class TeamcityLogger : ILogger
    {
        public void WriteLine(string message)
        {
           Console.WriteLine($"##teamcity[message text='{message}']");
        }
    }
}