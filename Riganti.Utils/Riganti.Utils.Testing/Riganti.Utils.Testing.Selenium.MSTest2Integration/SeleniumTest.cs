using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Represents implementation of base for selenium tests for MSTest. 
    /// </summary>
    public class SeleniumTest : SeleniumTestBase
    {
        
        public TestContext TestContext { get; set; }


        protected override TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration)
        {
            return new TestSuiteRunner(configuration, new TestContextProvider());
        }

        [ClassCleanup]
        public override void TotalCleanUp()
        {
            base.TotalCleanUp();
        }
    }
}