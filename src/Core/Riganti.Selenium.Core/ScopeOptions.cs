using System;
using OpenQA.Selenium;

namespace Riganti.Selenium.Core
{
    public class ScopeOptions
    {
        public Guid ScopeId { get; } = Guid.NewGuid();

        public BrowserWrapper Parent { get; set; }
        public string FrameSelector { get; set; }
        public string CurrentWindowHandle { get; set; }
        public Action<IWebDriver> ChangeScope { get; set; } 
    }
}