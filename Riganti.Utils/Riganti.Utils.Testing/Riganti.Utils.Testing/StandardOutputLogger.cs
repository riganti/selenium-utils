using System;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{

    /// <summary>
    /// Logger that writes by Console.WriteLine
    /// </summary>
    public class StandardOutputLogger :ILogger
    {
        public void WriteLine(string message,  TraceLevel level)
        {
            Console.WriteLine(message);
        }
    }
}