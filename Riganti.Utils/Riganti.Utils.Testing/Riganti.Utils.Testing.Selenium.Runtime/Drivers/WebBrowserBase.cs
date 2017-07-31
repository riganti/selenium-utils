using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
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

        
    }
}
