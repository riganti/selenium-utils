using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace Riganti.Selenium.Core.UnitTests.Mock
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
            throw new NotImplementedException();
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public string GetCssValue(string propertyName)
        {
            throw new NotImplementedException();
        }

        public string GetDomAttribute(string attributeName)
        {
            throw new NotImplementedException();
        }

        public string GetDomProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public ISearchContext GetShadowRoot()
        {
            throw new NotImplementedException();
        }

        public string TagName { get; set; } = "div";
        public string Text { get; set; } = "";
        public bool Enabled { get; set; } = true;
        public bool Selected { get; set; }
        public Point Location { get; set; } = new Point(0, 0);
        public Size Size { get; set; } = new Size(20, 20);
        public bool Displayed { get; set; } = true;
    }
}