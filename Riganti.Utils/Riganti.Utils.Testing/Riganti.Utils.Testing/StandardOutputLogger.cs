using System;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class StandardOutputLogger :ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}