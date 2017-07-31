using Riganti.Utils.Testing.Selenium.Core;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockITestInstance : ITestInstance
    {
        public ISeleniumTest TestClass { get; } = new MockISeleniumTest();
        public TestConfiguration TestConfiguration { get; } = new TestConfiguration() { BaseUrl = "http://localhost:1234", Factory = new MockIWebBrowserFactory() };
    }
}