using System;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
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

    public class XunitTestSuiteRunner : TestSuiteRunner
    {
        public XunitTestSuiteRunner(SeleniumTestsConfiguration configuration, TestContextProvider testContextProvider) : base(configuration, testContextProvider)
        {
        }

        public override void RunInAllBrowsers(ISeleniumTest testClass, Action<IBrowserWrapper> action, string callerMemberName, string callerFilePath,
            int callerLineNumber)
        {
            var context = (TestContextWrapper)TestContextAccessor.GetTestContext();
            context.TestName = callerMemberName;

            base.RunInAllBrowsers(testClass, action, callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}