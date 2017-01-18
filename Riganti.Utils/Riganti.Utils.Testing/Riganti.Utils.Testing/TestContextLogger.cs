using System.Diagnostics;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TestContextLogger : ILogger
    {
        public SeleniumTestBase TestBase { get; }

        public TestContextLogger(SeleniumTestBase test)
        {
            TestBase = test;
        }
        public void WriteLine(string message, TraceLevel level)
        {
            TestBase?.Context?.WriteLine(message);
        }
    }
}