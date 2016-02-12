using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ChromeFastModeDriver : SelfCleanUpWebDriver
    {
        protected override IWebDriver CreateInstance()
        {
            var driver = new DefaultChromeWebDriverFactory().CreateNewInstance();
            SeleniumTestBase.Log("ChromeFastModeDriver : DriverId = " + driver.GetDriverId());
            return driver;
        }
        
    }
}