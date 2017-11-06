using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.DotVVM;

namespace Selenium.DotVVM.Samples.Tests
{
    [TestClass]
    public class PostbackTests : AppSeleniumTest
    {
        [Timeout(15000)]
        [TestMethod]
        public void WaitForPostbackTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.Click("#WaitButton");
                browser.WaitForPostback();
                browser.First("#LabelText").CheckIfInnerTextEquals("PostbackEnd");
            });
        }
    }

}