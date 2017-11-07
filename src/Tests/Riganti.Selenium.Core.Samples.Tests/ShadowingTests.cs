using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class ShadowingTests : AppSeleniumTest
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
        [TestMethod]
        public void ElementWrapperCollectionShadow_FirstFuncTest()
        {
            RunInAllBrowsers(browser =>
            {
                //checking API

                browser.NavigateToUrl("/test/FindElements");
                var a = browser.FindElements("p");

                var b = a.First();
                b.CheckIfIsDisplayed();
            });
        }

        [TestMethod]
        public void ElementWrapperCollectionShadow_ThrowAndFirstFuncTest()
        {
            RunInAllBrowsers(browser =>
            {
                //checking API
                browser.NavigateToUrl("/test/FindElements");
                browser.FindElements("p").ThrowIfDifferentCountThan(3)
                    .First().CheckIfIsDisplayed();
            });
        }


    }
}