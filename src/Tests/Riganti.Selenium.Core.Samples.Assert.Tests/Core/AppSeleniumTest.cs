using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.UnitTests.Mock;
using Riganti.Selenium.xUnitIntegration;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public abstract class AppSeleniumTest : SeleniumTest
    {
        public void RunInAllBrowsers(Action<IBrowserWrapper> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
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

        public IBrowserWrapper CreateMockedIBrowserWrapper(MockIWebDriver driver = null)
        {
            driver = driver ?? new MockIWebDriver();
            return new BrowserWrapper(new MockIWebBrowser(driver), driver, new MockITestInstance(() => new TestContextProvider(),
                test =>
                {
                    test.TestSuiteRunner.ServiceFactory.RegisterTransient<IBrowserWrapper, BrowserWrapper>();
                    test.TestSuiteRunner.ServiceFactory.RegisterTransient<IElementWrapper, ElementWrapper>();
                    test.TestSuiteRunner.ServiceFactory.RegisterTransient<ISeleniumWrapperCollection, ElementWrapperCollection<IElementWrapper, IBrowserWrapper>>();
                }), new ScopeOptions());
        }

    }
}
