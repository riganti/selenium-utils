﻿using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.DotVVM
{
    public static class DotVVMAssert
    {
        public static void UploadFile(IElementWrapper element, string fullFileName)
        {

            if (element.BrowserWrapper.IsDotvvmPage())
            {
                element.BrowserWrapper.LogVerbose("Selenium.DotVVM : Uploading file");
                var isLessThenDotvvm30 = element.BrowserWrapper.GetJavaScriptExecutor()
                 .ExecuteScript("return !!(window[\"dotvvm\"] && dotvvm && dotvvm.fileUpload && dotvvm.fileUpload.createUploadId )|| false") as bool? ?? false;

                if (isLessThenDotvvm30)
                {
                    element.BrowserWrapper.LogVerbose("Selenium.DotVVM : Uploading file");
                    var name = element.GetTagName();
                    var iframeCount = element.FindElements("iframe", SelectBy.CssSelector).Count;
                    if (name == "a" && element.HasAttribute("onclick") && (element.GetAttribute("onclick")?.Contains("showUploadDialog") ?? false))
                    {
                        if (iframeCount == 1)
                        {
                            UploadFileByA(element, fullFileName);
                            return;
                        }
                        else
                        {
                            element = element.ParentElement.ParentElement;
                        }
                    }

                    if (name == "div" && iframeCount == 1)
                    {
                        UploadFileByDiv(element, fullFileName);
                        return;
                    }
                    else
                    {
                        element.BrowserWrapper.LogVerbose("Selenium.DotVVM : Cannot identify DotVVM scenario. Uploading over standard procedure.");

                        element.BrowserWrapper.OpenInputFileDialog(element, fullFileName);
                        return;
                    }
                }
                else
                {
                    var name = element.GetTagName();
                    if (name == "a" && element.HasAttribute("onclick") && (element.GetAttribute("onclick")?.Contains("showUploadDialog") ?? false))
                    {
                        element = element.ParentElement.ParentElement;
                    }

                    if (element.GetTagName() == "div")
                    {
                        var fileInput = element.Single("input[type=file]");
                        fileInput.SendKeys(fullFileName);

                        element.Wait(element.ActionWaitTime);
                        return;
                    }

                    element.BrowserWrapper.LogVerbose("Selenium.DotVVM : Cannot identify DotVVM scenario. Uploading over standard procedure.");
                }
                
            }

            element.BrowserWrapper.OpenInputFileDialog(element, fullFileName);
        }

        private static void UploadFileByDiv(IElementWrapper element, string fullFileName)
        {
            element.BrowserWrapper.GetJavaScriptExecutor()
           .ExecuteScript("dotvvm.fileUpload.createUploadId(arguments[0])", element.First("a", SelectBy.CssSelector).WebElement);

            var iframe = element.First("iframe", SelectBy.CssSelector).WebElement;
            element.BrowserWrapper.Driver.SwitchTo().Frame(iframe);

            var fileInput = element.BrowserWrapper._GetInternalWebDriver()
                .FindElement(SelectBy.CssSelector("input[type=file]"));
            fileInput.SendKeys(fullFileName);

            element.Wait(element.ActionWaitTime);
            element.ActivateScope();

        }

        private static void UploadFileByA(IElementWrapper element, string fullFileName)
        {
            element.BrowserWrapper.GetJavaScriptExecutor()
                .ExecuteScript("dotvvm.fileUpload.createUploadId(arguments[0])", element.WebElement);

            var iframe = element.ParentElement.ParentElement.First("iframe", SelectBy.CssSelector).WebElement;
            element.BrowserWrapper.Driver.SwitchTo().Frame(iframe);

            var fileInput = element.BrowserWrapper._GetInternalWebDriver()
                .FindElement(SelectBy.CssSelector("input[type=file]"));
            fileInput.SendKeys(fullFileName);

            element.Wait(element.ActionWaitTime);
            element.ActivateScope();
        }
    }
}
