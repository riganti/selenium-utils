using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core;
using Riganti.Selenium.DotVVM;

namespace Selenium.DotVVM.Samples.Tests
{
    [TestClass]
    public class DotvvmDetectionTests : AppSeleniumTest
    {
        [TestMethod]
        public void DetectDotvvm()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Assert.IsTrue(browser.IsDotvvmPage());
                browser.NavigateToUrl("//google.com/?query=wallpaper");
                browser.Wait(1000);
                Assert.IsFalse(browser.IsDotvvmPage());
            });
        }
    }
}