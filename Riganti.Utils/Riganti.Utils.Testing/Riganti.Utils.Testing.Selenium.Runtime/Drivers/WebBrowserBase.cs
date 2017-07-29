using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
    public abstract class WebBrowserBase : IWebBrowser
    {

        private IWebDriver driver = null;

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    driver = CreateDriver();
                }
                return driver;
            }
            protected set
            {
                driver = value;
            }
        }

        protected abstract IWebDriver CreateDriver();

        public abstract void ClearBrowserState();

        public void Dispose()
        {
            driver?.Dispose();
        }
    }
}
