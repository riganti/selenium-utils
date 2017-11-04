using System;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Factories;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core.Drivers
{
    public interface IWebBrowser : IDisposable
    {

        string UniqueName { get; }

        IWebDriver Driver { get; }

        IWebBrowserFactory Factory { get; }
        
    }
}