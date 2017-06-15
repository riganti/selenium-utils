using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockIWebDriver : IWebDriver
    {
        public Func<IList<IWebElement>> FindElementsAction { get; set; }
        public IWebElement FindElement(By @by)
        {
            return FindElements(by).First();

        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            var enm = FindElementsAction?.Invoke();
            return new ReadOnlyCollection<IWebElement>(enm ?? new List<IWebElement>());

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

            return new MockITargetLocaltor() { CurrentDriver = this };
        }


        public string Url { get; set; } = "https://localhost:12345/path1/path2?query=1#fragment";
        public string Title { get; } = nameof(MockIWebDriver);
        public string PageSource { get; } = nameof(MockIWebDriver);
        public string CurrentWindowHandle { get; } = nameof(MockIWebDriver);
        public ReadOnlyCollection<string> WindowHandles { get; }
    }

    public class MockITargetLocaltor : ITargetLocator
    {
        public IWebDriver Frame(int frameIndex)
        {
            return CurrentDriver;
        }

        public IWebDriver Frame(string frameName)
        {
            return CurrentDriver;

        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            return CurrentDriver;

        }

        public IWebDriver ParentFrame()
        {
            return CurrentDriver;

        }

        public IWebDriver Window(string windowName)
        {
            return CurrentDriver;
        }

        public IWebDriver DefaultContent()
        {
            return CurrentDriver;
        }

        public IWebElement ActiveElement()
        {
            return null;

        }

        public IAlert Alert()
        {
            return null;
        }

        public MockIWebDriver CurrentDriver { get; set; }
    }
}