using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public sealed class InternetExplorerFastModeFactoryBase : IFastModeFactory
    {
        private static InternetExplorerFastModeDriver Driver { get; set; }
        private static readonly object Locker = new object();

        public IWebDriver CreateNewInstance()
        {
            if (Driver == null)
            {
                lock (Locker)
                {
                    if (Driver == null)
                    {
                        Driver = new InternetExplorerFastModeDriver();
                    }
                }
            }
            return Driver.Driver;
        }

        public void Clear()
        {
            Driver?.Clear();
        }

        public void Dispose()
        {
            Driver?.Dispose();
        }

        public void Recreate()
        {
            Driver?.Recreate();
        }
    }
}