using System;
using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{

    /// <summary>
    /// Logger that writes by Console.WriteLine
    /// </summary>
    public class StandardOutputLogger :ILogger
    {
        public void WriteLine(string message,  TraceLevel level)
        {
            Console.WriteLine(message);
        }
        public void OnTestFinished(ITestContext context)
        {
            //ignore
        }
    }
    /// <summary>
    /// Logger that writes all incomming messages into file which is added to results of test as a file with name "test.log".
    /// </summary>
    public class TestResultFileLogger : ILogger
    {
        public void WriteLine(string message, TraceLevel level)
        {
            
        }
        public void OnTestFinished(ITestContext context)
        {
            //context.ResultsDirectory
        }
    }
}