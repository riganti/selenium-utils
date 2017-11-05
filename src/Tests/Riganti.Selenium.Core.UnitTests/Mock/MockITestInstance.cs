using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.FluentApi;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockITestInstance : ITestInstance
    {
        public ISeleniumTest TestClass { get; } = new MockISeleniumTest()
        {
            TestSuiteRunner = new TestSuiteRunner(new SeleniumTestsConfiguration(){},new TestContextProvider())
        };
         

        public TestConfiguration TestConfiguration { get; } = new TestConfiguration() { BaseUrl = "http://localhost:1234", Factory = new MockIWebBrowserFactory() };

        public MockITestInstance()
        {
            TestClass.TestSuiteRunner.ServiceFactory.RegisterTransient<IBrowserWrapper, BrowserWrapperFluentApi>();
            TestClass.TestSuiteRunner.ServiceFactory.RegisterTransient<IElementWrapper, ElementWrapperFluentApi>();
        }
    }
}