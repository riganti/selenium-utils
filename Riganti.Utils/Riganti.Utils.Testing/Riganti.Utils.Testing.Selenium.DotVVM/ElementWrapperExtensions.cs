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
                SeleniumTestBase.Log("Selenium.DotVVM : Uploading file", 10);
                var name = element.GetTagName();
                if (name == "a" && element.HasAttribute("onclick") && (element.GetAttribute("onclick")?.Contains("showUploadDialog") ?? false))
                {
                    return UploadFileByA(element, fullFileName);
                }

                if (name == "div" && element.FindElements("iframe", SelectBy.CssSelector).Count == 1)
                {
                    return UploadFileByDiv(element, fullFileName);
                }
                else
                {
                    SeleniumTestBase.Log("Selenium.DotVVM : Cannot identify DotVVM scenario. Uploading over standard procedure.", 10);

                    element.BrowserWrapper.FileUploadDialogSelect(element, fullFileName);
                    return element;
                }
            }

            element.BrowserWrapper.FileUploadDialogSelect(element, fullFileName);
            return element;
        }

        private static ElementWrapper UploadFileByDiv(ElementWrapper element, string fullFileName)
        {
            element.BrowserWrapper.GetJavaScriptExecutor()
           .ExecuteScript("dotvvm.fileUpload.createUploadId(arguments[0])", element.First("a", SelectBy.CssSelector).WebElement);

            var iframe = element.First("iframe", SelectBy.CssSelector);
            element.BrowserWrapper.Browser.SwitchTo().Frame(iframe.WebElement);

            var fileInput = element.BrowserWrapper._GetInternalWebDriver()
                .FindElement(SelectBy.CssSelector("input[type=file]"));
            fileInput.SendKeys(fullFileName);

            element.Wait(element.ActionWaitTime);
            element.ActivateScope();
            return element;
        }

        private static ElementWrapper UploadFileByA(ElementWrapper element, string fullFileName)
        {
            element.BrowserWrapper.GetJavaScriptExecutor()
                .ExecuteScript("dotvvm.fileUpload.createUploadId(arguments[0])", element.WebElement);

            var iframe = element.ParentElement.ParentElement.First("iframe", SelectBy.CssSelector);
            element.BrowserWrapper.Browser.SwitchTo().Frame(iframe.WebElement);

            var fileInput = element.BrowserWrapper._GetInternalWebDriver()
                .FindElement(SelectBy.CssSelector("input[type=file]"));
            fileInput.SendKeys(fullFileName);

            element.Wait(element.ActionWaitTime);
            element.ActivateScope();
            return element;
        }
    }
}