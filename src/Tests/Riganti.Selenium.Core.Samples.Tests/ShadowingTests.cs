using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class ShadowingTests: AppSeleniumTest
    {
        [TestMethod]
        public void ElementWrapperShadow_SingleFuncTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                var a = browser.First("div");
                var b = a.FindElements("p");
                var e = b.ElementAt(0);
                e.CheckIfInnerTextEquals("a");
            });
        }

        [TestMethod]
        public void ElementWrapperShadow_GetEnumerableFuncTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                var a = browser.First("div");
                var b = a.FindElements("p");

                foreach (var p in b)
                {
                    p.CheckIfIsDisplayed();
                }

            });
        }

    }
}