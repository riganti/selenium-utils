using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace Selenium.Core.UnitTests.Mock
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

        public string TagName { get; set; }
        public string Text { get; set; }
        public bool Enabled { get; set; }
        public bool Selected { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public bool Displayed { get; set; }
    }
}