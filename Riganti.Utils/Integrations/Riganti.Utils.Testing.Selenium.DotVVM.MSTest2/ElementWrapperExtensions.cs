using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.DotVVM
{
    public static class ElementWrapperExtensions
    {
        public static IElementWrapper UploadFile(this IElementWrapper element, string fullFileName)
        {
            throw new NotImplementedException();
            //if (element.BrowserWrapper.IsDotvvmPage())
            //{
            //    element.BrowserWrapper.LogVerbose("Selenium.DotVVM : Uploading file");
            //    var name = element.GetTagName();
            //    if (name == "a" && element.HasAttribute("onclick") && (element.GetAttribute("onclick")?.Contains("showUploadDialog") ?? false))
            //    {
            //        return UploadFileByA(element, fullFileName);
            //    }

            //    if (name == "div" && element.FindElements("iframe", SelectBy.CssSelector).Count == 1)
            //    {
            //        return UploadFileByDiv(element, fullFileName);
            //    }
            //    else
            //    {
            //        element.BrowserWrapper.LogVerbose("Selenium.DotVVM : Cannot identify DotVVM scenario. Uploading over standard procedure.");

            //        element.BrowserWrapper.FileUploadDialogSelect(element, fullFileName);
            //        return element;
            //    }
            //}

            //element.BrowserWrapper.FileUploadDialogSelect(element, fullFileName);
            //return element;
        }

        private static IElementWrapper UploadFileByDiv(IElementWrapper element, string fullFileName)
        {
            element.BrowserWrapper.GetJavaScriptExecutor()
           .ExecuteScript("dotvvm.fileUpload.createUploadId(arguments[0])", element.First("a", SelectBy.CssSelector).WebElement);

            var iframe = element.First("iframe", SelectBy.CssSelector);
            element.BrowserWrapper.Driver.SwitchTo().Frame(iframe.WebElement);

            var fileInput = element.BrowserWrapper._GetInternalWebDriver()
                .FindElement(SelectBy.CssSelector("input[type=file]"));
            fileInput.SendKeys(fullFileName);

            element.Wait(element.ActionWaitTime);
            element.ActivateScope();
            return element;
        }

        private static IElementWrapper UploadFileByA(IElementWrapper element, string fullFileName)
        {
            element.BrowserWrapper.GetJavaScriptExecutor()
                .ExecuteScript("dotvvm.fileUpload.createUploadId(arguments[0])", element.WebElement);

            var iframe = element.ParentElement.ParentElement.First("iframe", SelectBy.CssSelector);
            element.BrowserWrapper.Driver.SwitchTo().Frame(iframe.WebElement);

            var fileInput = element.BrowserWrapper._GetInternalWebDriver()
                .FindElement(SelectBy.CssSelector("input[type=file]"));
            fileInput.SendKeys(fullFileName);

            element.Wait(element.ActionWaitTime);
            element.ActivateScope();
            return element;
        }
    }
}