using System;
using Riganti.Selenium.Core;

namespace Riganti.Selenium.DotVVM
{
    public static class BrowserWrapperExtensions
    {
        public static bool IsDotvvmPage(this BrowserWrapper browser)
        {
            try
            {
                return string.Equals("true",
                    browser.GetJavaScriptExecutor().ExecuteScript("return dotvvm instanceof DotVVM").ToString(),
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}