using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core;

namespace Riganti.Utils.Testing.Selenium.DotVVM
{
    public static class ElementWrapperExtensions
    {
        public static ElementWrapper UploadFile(this ElementWrapper element, string fullFileName)
        {

            if (element.BrowserWrapper.IsDotvvmPage())
            {
                
            }
            else
            {
                element.BrowserWrapper.FileUploadDialogSelect(element, fullFileName);
            }
            return element;
        }

    }
    public static class BrowserWrapperExtensions
    {
        public static bool IsDotvvmPage(this BrowserWrapper browser)
        {
            try
            {
                return string.Equals("true",
                    browser.GetJavaScriptExecutor().ExecuteScript("dotvvm instanceof DotVVM").ToString(),
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
