using System;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core.UnitTests.Mock
{
    public class MockITestInstance : ITestInstance
    {
        public ISeleniumTest TestClass { get; }


        public TestConfiguration TestConfiguration { get; } = new TestConfiguration() { BaseUrl = "http://localhost:1234", Factory = new MockIWebBrowserFactory() };

        public MockITestInstance(Func<ITestContextProvider> factoryMethod, Action<ISeleniumTest> configure)
        {
            TestClass = new MockISeleniumTest() { TestSuiteRunner = new TestSuiteRunner(new SeleniumTestsConfiguration() { }, factoryMethod()) };
            configure(TestClass);
        }
    }
}