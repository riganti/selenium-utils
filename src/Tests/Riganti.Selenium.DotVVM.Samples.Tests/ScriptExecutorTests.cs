using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Selenium.DotVVM;

namespace Selenium.DotVVM.Samples.Tests
{
    [TestClass]
    public class ScriptExecutorTests : AppSeleniumTest
    {
        [TestMethod]
        public void RunScriptExecutor()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.IsDotvvmPage();

                browser.GetJavaScriptExecutor().ExecuteScript("console.log('Calling WaitButton');" +
                                                              "var a = document.querySelector('#WaitButton');" +
                                                              "console.log(a); a.click()");

                browser.WaitFor(() =>
                {
                    browser.First("#LabelText").CheckIfInnerTextEquals("PostbackEnd");
                }, 60000);
            });
        }
    }
}