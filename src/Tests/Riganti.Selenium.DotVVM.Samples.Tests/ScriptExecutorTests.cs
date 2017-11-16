using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                browser.GetJavaScriptExecutor().ExecuteScript("$('#WaitButton').click()");

                browser.WaitFor(() =>
                {
                    browser.First("#LabelText").CheckIfInnerTextEquals("PostbackEnd");
                }, 5000);
            });
        }
    }
}
