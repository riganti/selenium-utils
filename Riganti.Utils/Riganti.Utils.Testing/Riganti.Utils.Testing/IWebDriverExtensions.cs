using System;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public static class IWebDriverExtensions
    {
        public static void SetDefaultTimeouts(this IWebDriver browser)
        {
            //var timeouts = browser.Manage().Timeouts();
            //timeouts.SetPageLoadTimeout(TimeSpan.FromSeconds(15));
            //timeouts.ImplicitlyWait(TimeSpan.FromMilliseconds(150));
        }

    }
}