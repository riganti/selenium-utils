using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public sealed class FirefoxFastModeFactoryBase : IFastModeFactory
    {
        private static FirefoxFastModeDriver Driver { get; set; }
        private static readonly object Locker = new object();

        public IWebDriver CreateNewInstance()
        {
            if (Driver == null)
            {
                lock (Locker)
                {
                    if (Driver == null)
                    {
                        Driver = new FirefoxFastModeDriver();
                    }
                }
            }
            return Driver.Driver;
        }

        public void Clear()
        {
            Driver.Clear();
            
        }

        public void Dispose()
        {
            Driver.Dispose();
        }

        public void Recreate()
        {
            Driver.Recreate();
        }
    }
}