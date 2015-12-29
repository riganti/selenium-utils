using System.Diagnostics;

namespace Riganti.Utils.Testing.SeleniumCore
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