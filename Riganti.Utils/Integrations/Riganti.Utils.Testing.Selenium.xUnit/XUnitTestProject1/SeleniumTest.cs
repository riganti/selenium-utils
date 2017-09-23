using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.xUnitIntegration;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Represents implementation of base for selenium tests for MSTest. 
    /// </summary>
    public abstract class SeleniumTest : SeleniumTestExecutor
    {
        public ITestOutputHelper TestOutput { get; set; }

        public SeleniumTest(ITestOutputHelper output)
        {
            TestOutput = output;
        }


        protected override TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration)
        {
            return new XunitTestSuiteRunner(configuration, new TestContextProvider());
        }

    }
}