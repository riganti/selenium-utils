using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Factories;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core.Drivers
{
    public interface IWebBrowser : IDisposable
    {

        string UniqueName { get; }

        IWebDriver Driver { get; }

        IWebBrowserFactory Factory { get; }
        
    }
}