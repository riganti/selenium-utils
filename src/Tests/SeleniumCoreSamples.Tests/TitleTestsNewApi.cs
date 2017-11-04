using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Api;
using Riganti.Selenium.Core.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class TitleTestsNewApi : SeleniumTest
    {
        [TestMethod]
        public void CheckIfTitleEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfTitleEquals(browser, "This is title       ");
            });
        }
        [TestMethod]
        public void CheckIfTitleNotEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfTitleNotEquals(browser, "This is not title            ");
            });
        }
        [TestMethod]
        public void CheckIfTitleEquals_trim()
        {
            ExpectException(typeof(BrowserException));
            RunInAllBrowsers(browser =>
            {

                browser.NavigateToUrl();
                AssertUI.CheckIfTitleEquals(browser, "This is title           ", trim: false);
            });
        }
        [TestMethod]
        public void CheckIfTitleNotEquals_Exception()
        {
            ExpectException(typeof(BrowserException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfTitleNotEquals(browser, "This is title");
            });
        }
        [TestMethod]
        public void CheckIfTitleEquals_Exception()
        {
            ExpectException(typeof(BrowserException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfTitleEquals(browser, "This is not title");
            });
        }
        [TestMethod]
        public void CheckIfTitle_Exception()
        {
            ExpectException(typeof(BrowserException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                AssertUI.CheckIfTitle(browser, c => c == "This is not title");
            });
        }
    }
}