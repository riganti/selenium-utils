using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface ISelfCleanUpWebDriver
    {
        IWebDriver Driver { get; }

        void Clear();
        void Dispose();
        void Recreate();
    }
}