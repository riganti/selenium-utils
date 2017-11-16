using System;
using System.Linq;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.xUnitIntegration
{
    public class XunitTestSuiteRunner : TestSuiteRunner
    {
        public XunitTestSuiteRunner(SeleniumTestsConfiguration configuration, TestContextProvider testContextProvider) : base(configuration, (ITestContextProvider) testContextProvider)
        {
        }

        public override void RunInAllBrowsers(ISeleniumTest testClass, Action<IBrowserWrapper> action, string callerMemberName, string callerFilePath,
            int callerLineNumber)
        {
            //TODO: make a review
            var context = (TestContextWrapper)TestContextProvider.GetGlobalScopeTestContext();
            context.TestName = callerMemberName;
            context.FullyQualifiedTestClassName = new System.Diagnostics.StackTrace().GetFrames()?.Select(s => s.GetMethod()?
                .ReflectedType?.FullName).FirstOrDefault(s => !s?.Contains(this.GetType()?.Namespace ?? "") ?? false);
            base.RunInAllBrowsers(testClass, action, callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}