using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Represents implementation of base for selenium tests for MSTest. 
    /// </summary>
    public class SeleniumTest : SeleniumTestExecutor
    {

        public TestContext TestContext { get; set; }


        protected override TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration)
        {
            var testContextProvider = new TestContextProvider();
            testContextProvider.SetContext(TestContext);
            return new TestSuiteRunner(configuration, testContextProvider);
        }

    }
}