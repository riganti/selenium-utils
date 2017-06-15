using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ISelfCleanUpWebDriver
    {
        IWebDriver Driver { get; }

        void Clear();
        void Dispose();
        void Recreate();
    }
}