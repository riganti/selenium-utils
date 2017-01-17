using OpenQA.Selenium;
using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public sealed class ChromeFastModeFactory : IFastModeFactory
    {
        private static ChromeFastModeDriver Driver { get; set; }
        private static readonly object Locker = new object();

        public IWebDriver CreateNewInstance()
        {
            if (Driver == null)
            {
                lock (Locker)
                {
                    if (Driver == null)
                    {
                        Driver = new ChromeFastModeDriver();
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