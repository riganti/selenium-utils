using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Sandbox
{
    /// <summary>
    /// Represents implementation of base for selenium tests for MSTest.
    /// </summary>
    public class SeleniumTest : SeleniumTestExecutor
    {
        protected override TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration)
        {
            var testContextProvider = new TestContextProvider();
            return CreateNewTestSuiteRunner(configuration, testContextProvider);
        }

        protected virtual TestSuiteRunner CreateNewTestSuiteRunner(SeleniumTestsConfiguration configuration, TestContextProvider testContextProvider)
        {
            return new TestSuiteRunner(configuration, testContextProvider);
        }
    }
}
