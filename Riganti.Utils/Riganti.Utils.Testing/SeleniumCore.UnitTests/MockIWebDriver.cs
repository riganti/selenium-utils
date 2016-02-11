using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SeleniumCore.UnitTests
{
    public class MockIWebDriver : IWebDriver
    {
        public IWebElement FindElement(By @by)
        {
            return null;
        }

        public ReadOnlyCollection<string> FindElements(By @by)
        {
            return null;
        }

        public void Dispose()
        {
        }

        public void Close()
        {
        }

        public void Quit()
        {
        }

        public IOptions Manage()
        {
            return null;
        }

        public INavigation Navigate()
        {
            return null;
        }

        public ITargetLocator SwitchTo()
        {
            return null;
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            return null;
        }

        public string Url { get; set; } = "https://localhost:12345/path1/path2?query=1#fragment";
        public string Title { get; } = nameof(MockIWebDriver);
        public string PageSource { get; } = nameof(MockIWebDriver);
        public string CurrentWindowHandle { get; } = nameof(MockIWebDriver);
        public ReadOnlyCollection<string> WindowHandles { get; }
    }
}