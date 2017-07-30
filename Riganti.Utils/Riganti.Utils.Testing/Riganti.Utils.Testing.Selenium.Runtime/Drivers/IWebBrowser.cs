using System;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
    public interface IWebBrowser : IDisposable
    {

        IWebDriver Driver { get; }

        void ClearDriverState();


    }
}