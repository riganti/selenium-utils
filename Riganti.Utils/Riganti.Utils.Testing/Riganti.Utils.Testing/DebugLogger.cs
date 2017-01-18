using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class DebugLogger : ILogger
    {
        public void WriteLine(string message, TraceLevel level)
        {
            Debug.WriteLine(message, "Test");
            Debug.Flush();
        }
    }
}