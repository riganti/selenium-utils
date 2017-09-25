using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Logging
{
    public class TextFileLogger : ILogger
    {
        public string Name => "textFile";

        public IDictionary<string, string> Options { get; }

        private string LogFileName => Options["logFileName"];


        public void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            File.AppendAllText(LogFileName, $"[{level}] {message}\r\n");
        }

        public void OnTestStarted(ITestContext instanceContext)
        {
        }

        public void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}