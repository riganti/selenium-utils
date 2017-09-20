using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Riganti.Utils.Testing.Selenium.Core.Logging
{
    public class TextFileLogger : ILogger
    {
        public string Name => "textFile";

        public IDictionary<string, string> Options { get; }

        private string LogFileName => Options["logFileName"];


        public void WriteLine(ITestContext context, string message, TraceLevel level)
        {
            File.AppendAllText(LogFileName, $"[{level}] {message}\r\n");
        }

        public void OnTestStarted(ITestContext context)
        {
        }

        public void OnTestFinished(ITestContext context)
        {
        }
    }
}