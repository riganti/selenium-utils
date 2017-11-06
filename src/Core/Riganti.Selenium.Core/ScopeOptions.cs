using System;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public class ScopeOptions
    {
        public Guid ScopeId { get; } = Guid.NewGuid();

        public IBrowserWrapper Parent { get; internal set; }
        public string FrameSelector { get; internal set; }
        public string CurrentWindowHandle { get; internal set; }
        public Action<IWebDriver> ChangeScope { get; internal set; }

    }


}