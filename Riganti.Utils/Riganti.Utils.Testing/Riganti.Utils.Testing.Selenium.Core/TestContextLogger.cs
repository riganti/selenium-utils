using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Logger that writes incomming messages into TestContext.
    /// </summary>
    public class TestContextLogger : ILogger
    {
        private ISeleniumTest TestBase { get; }

        public TestContextLogger(SeleniumTestBase test)
        {
            TestBase = test;
        }
        public void WriteLine(string message, TraceLevel level)
        {
            TestBase?.Context?.WriteLine(message);
        }
        public void OnTestFinished(ITestContext context)
        {
            //ignore
        }
    }
}