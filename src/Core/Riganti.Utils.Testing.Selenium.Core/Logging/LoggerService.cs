using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Discovery;

namespace Riganti.Utils.Testing.Selenium.Core.Logging
{
    public class LoggerService
    {

        private readonly List<ILogger> loggers;
        public IEnumerable<ILogger> Loggers => loggers;


        public LoggerService(List<ILogger> loggers)
        {
            this.loggers = loggers;
        }


        /// <summary>
        /// Write message to log.
        /// </summary>
        /// <param name="instanceContext">Current test instanceContext.</param>
        /// <param name="message">Text to write.</param>
        /// <param name="level">Message information level.</param>
        public void WriteLine(ITestContext instanceContext, string message, TraceLevel level)
        {
            RunOnAllLoggers(l => l.WriteLine(instanceContext, message, level));
        }

        /// <summary>
        /// This method is called when test started.
        /// </summary>
        public void OnTestStarted(ITestContext instanceContext)
        {
            RunOnAllLoggers(l => l.OnTestStarted(instanceContext));
        }

        /// <summary>
        /// This method is called when test finished.
        /// </summary>
        public void OnTestFinished(ITestContext instanceContext)
        {
            RunOnAllLoggers(l => l.OnTestFinished(instanceContext));
        }


        private void RunOnAllLoggers(Action<ILogger> action)
        {
            foreach (var logger in loggers)
            {
                try
                {
                    action(logger);
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}
