using System;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.xUnitIntegration
{
    public class XunitTestSuiteRunner : TestSuiteRunner
    {
        public XunitTestSuiteRunner(SeleniumTestsConfiguration configuration, TestContextProvider testContextProvider) : base(configuration, (ITestContextProvider) testContextProvider)
        {
        }

        public override void RunInAllBrowsers(ISeleniumTest testClass, Action<IBrowserWrapper> action, string callerMemberName, string callerFilePath,
            int callerLineNumber)
        {
            var context = (TestContextWrapper)TestContextProvider.GetGlobalScopeTestContext();
            context.TestName = callerMemberName;
            context.FullyQualifiedTestClassName = new System.Diagnostics.StackTrace().GetFrames()?.Select(s => s.GetMethod()?
                .ReflectedType?.FullName).FirstOrDefault(s => !s?.Contains(this.GetType()?.Namespace ?? "") ?? false);

            base.RunInAllBrowsers(testClass, action, callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}