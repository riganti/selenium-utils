using Riganti.Selenium.Core.Abstractions;
using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.xUnitIntegration;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core;
using Xunit.Abstractions;
using Riganti.Selenium.Core.Configuration;
using System.Reflection;

namespace Riganti.Selenium.DotVVM3.Samples.Tests
{
    public class AppSeleniumTest : SeleniumTest
    {
        public void RunInAllBrowsers(Action<IBrowserWrapper> testBody, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            AssertApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }
        public AppSeleniumTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override TestSuiteRunner CreateNewTestSuiteRunner(SeleniumTestsConfiguration configuration, TestContextProvider provider)
        {
            return new XunitTestSuiteRunner(configuration, provider, (service, runner) =>
            {
                runner.SearchAssemblies.Add(Assembly.GetExecutingAssembly());
            });
        }
    }
}