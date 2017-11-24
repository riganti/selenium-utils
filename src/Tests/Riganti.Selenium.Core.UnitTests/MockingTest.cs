using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.UnitTests.Mock;

namespace Selenium.Core.UnitTests
{
    public class MockingTest
    {
        public IBrowserWrapperFluentApi CreateMockedIBrowserWrapper(MockIWebDriver driver = null)
        {
            driver = driver ?? new MockIWebDriver();
            return new BrowserWrapperFluentApi(new MockIWebBrowser(driver), driver, new MockITestInstance(() => new TestContextProvider(),
                test =>
                {
                    test.TestSuiteRunner.ServiceFactory.RegisterTransient<IBrowserWrapper, BrowserWrapperFluentApi>();
                    test.TestSuiteRunner.ServiceFactory.RegisterTransient<IElementWrapper, ElementWrapperFluentApi>();
                    test.TestSuiteRunner.ServiceFactory.RegisterTransient<ISeleniumWrapperCollection, ElementWrapperCollection<IElementWrapperFluentApi, IBrowserWrapperFluentApi>>();
                }), new ScopeOptions());
        }

    }
}