using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.xUnitIntegration
{
    /// <summary>
    /// Represents implementation of base for selenium tests for MSTest. 
    /// </summary>
    public abstract class SeleniumTest : SeleniumTestExecutor
    {
        public ITestOutputHelper TestOutput { get; set; }

        public SeleniumTest(ITestOutputHelper output)
        {
            this.TestOutput = output;
        }


        protected override TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration)
        {
            return (TestSuiteRunner)new XunitTestSuiteRunner(configuration, new TestContextProvider());
        }

    }
}