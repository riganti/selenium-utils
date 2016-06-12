using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class StandardOutputLogger :ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}