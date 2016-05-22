using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace SeleniumCore.UnitTests.Mock
{
    public class MockIWebElement : IWebElement
    {
        public IWebElement FindElement(By @by)
        {
            return null;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return null;
        }

        public void Clear()
        {
        }

        public void SendKeys(string text)
        {
        }

        public void Submit()
        {
        }

        public void Click()
        {
        }

        public string GetAttribute(string attributeName)
        {
            return null;
        }

        public string GetCssValue(string propertyName)
        {
            return null;
        }

        public string TagName { get; }
        public string Text { get; }
        public bool Enabled { get; }
        public bool Selected { get; }
        public Point Location { get; }
        public Size Size { get; }
        public bool Displayed { get; }
    }
}