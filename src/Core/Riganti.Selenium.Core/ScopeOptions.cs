using System;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public class ScopeOptions
    {
        public Guid ScopeId { get; } = Guid.NewGuid();

        public IBrowserWrapper Parent { get; set; }
        public string FrameSelector { get; set; }
        public string CurrentWindowHandle { get; set; }
        public Action<IWebDriver> ChangeScope { get; set; } 
    }
}