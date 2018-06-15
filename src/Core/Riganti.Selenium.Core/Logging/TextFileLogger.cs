using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core.Logging
{
    public class TextFileLogger : ILogger
    {
        public virtual string Name => "textFile";

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        protected virtual string LogFileName => Options["logFileName"];


        public virtual void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            File.AppendAllText(LogFileName, $"[{level}] {message}\r\n");
        }

        public virtual void OnTestStarted(ITestContext instanceContext)
        {
        }

        public virtual void OnTestFinished(ITestContext instanceContext)
        {
        }
    }
}