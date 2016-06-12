using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class DebuggerLogger : ILogger
    {
        public void WriteLine(string message)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Log(1, Debugger.DefaultCategory, message);
            }
        }
    }
}