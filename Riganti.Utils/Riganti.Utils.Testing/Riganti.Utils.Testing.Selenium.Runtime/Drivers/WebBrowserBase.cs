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
        public IWebBrowserFactory Factory { get; }
        


        protected IWebDriver driverInstance = null;

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
        }


        protected abstract IWebDriver CreateDriver();
        
        public abstract void ClearDriverState();
        
        public virtual void RecreateDriver()
        {
            KillDriver();
            driverInstance = CreateDriver();
        }

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
