using OpenQA.Selenium;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.UnitTests.Mock
{
    public class MockIWebBrowser : IWebBrowser
    {
        public MockIWebBrowser()
        {
            Driver = new MockIWebDriver();
            Factory = new MockIWebBrowserFactory();
        }

        public MockIWebBrowser(IWebDriver driver)
        {
            Driver = driver;
            Factory = new MockIWebBrowserFactory();
        }

        public void Dispose()
        {
        }

        public string UniqueName => "mock";
        public IWebDriver Driver { get; }
        public IWebBrowserFactory Factory { get; }
    }
}