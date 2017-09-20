using System;
using Riganti.Utils.Testing.Selenium.Core;

namespace Riganti.Utils.Testing.Selenium.DotVVM
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