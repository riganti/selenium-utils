using System;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{

    /// <summary>
    /// Logger that writes by Debbuger.WriteLine.
    /// </summary>
    public class DebuggerLogger : ILogger
    {
        public void WriteLine(string message, TraceLevel level)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Log(0, Debugger.DefaultCategory, message);
            }
        }

        public void OnTestFinished(ITestContext context)
        {
            //ignore
        }
    }
}