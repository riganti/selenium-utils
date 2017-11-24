using OpenQA.Selenium;

namespace Riganti.Selenium.Core.UnitTests.Mock
{
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