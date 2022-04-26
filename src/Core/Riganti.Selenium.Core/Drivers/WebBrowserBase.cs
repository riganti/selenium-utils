using System;
using System.Diagnostics;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers
{
    /// <summary>
    /// Provides basic functions for generic implementation of web driver. 
    /// </summary>
    public abstract class WebBrowserBase : IWebBrowser
    {
        private static int uniqueBrowserIndex = 1;
        private static readonly object uniqueBroserIndexLocker = new object();



        public IWebBrowserFactory Factory { get; }
        
        protected IWebDriver driverInstance = null;

        public string UniqueName { get; }

        public IWebDriver Driver
        {
            get
            {
                if (driverInstance == null)
                {
                    driverInstance = CreateDriver();
                }
                return driverInstance;
            }
        }


        public WebBrowserBase(IWebBrowserFactory factory)
        {
            Factory = factory;

            lock (uniqueBroserIndexLocker)
            {
                UniqueName = factory + "-" + (uniqueBrowserIndex++);
            }
        }


        protected abstract IWebDriver CreateDriver();
        

        public virtual void KillDriver()
        {
            driverInstance?.Dispose();
        }

        public void Dispose()
        {
            KillDriver();
        }
        protected void StopWatchedAction(Action action, Action<Stopwatch> afterActionExecuted)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            afterActionExecuted(stopwatch);
        }

    }
}
