using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class DebugLogger : ILogger
    {
        public void WriteLine(string message)
        {
            Debug.WriteLine(message, "Test");
            Debug.Flush();
        }
    }
}