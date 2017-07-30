using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
    public interface IWebBrowser : IDisposable
    {

        IWebDriver Driver { get; }
        IWebBrowserFactory Factory { get; }

        void ClearDriverState();

        void RecreateDriver();
        
    }
}