using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Discovery;

namespace Riganti.Utils.Testing.Selenium.Runtime.Logging
{
    public class LoggerService
    {

        private readonly Dictionary<string, ILogger> loggers;

        public LoggerService(SeleniumTestsConfiguration configuration)
        {
            var discoveryService = new LoggerResolver();
            loggers = discoveryService.CreateLoggers(configuration, new[] { Assembly.GetExecutingAssembly() });
        }



        /// <summary>
        /// Write message to log.
        /// </summary>
        /// <param name="context">Current test context.</param>
        /// <param name="message">Text to write.</param>
        /// <param name="level">Message information level.</param>
        public void WriteLine(ITestContext context, string message, TraceLevel level)
        {
            RunOnAllLoggers(l => l.WriteLine(context, message, level));
        }

        /// <summary>
        /// This method is called when test started.
        /// </summary>
        public void OnTestStarted(ITestContext context)
        {
            RunOnAllLoggers(l => l.OnTestStarted(context));
        }

        /// <summary>
        /// This method is called when test finished.
        /// </summary>
        public void OnTestFinished(ITestContext context)
        {
            RunOnAllLoggers(l => l.OnTestFinished(context));
        }


        private void RunOnAllLoggers(Action<ILogger> action)
        {
            foreach (var logger in loggers.Values)
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
