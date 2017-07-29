using System;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
    public interface IWebBrowser : IDisposable
    {

        IWebDriver Driver { get; }

        void ClearBrowserState();


    }
}