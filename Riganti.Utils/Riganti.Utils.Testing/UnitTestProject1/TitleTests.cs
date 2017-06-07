﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace SeleniumCore.Samples.Tests
{

    [TestClass]
    public class TitleTests : SeleniumTest
    {
        [TestMethod]
        public void CheckIfTitleEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleEquals("This is title       ");
            });
        }
        [TestMethod]
        public void CheckIfTitleNotEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleNotEquals("This is not title            ");
            });
        }
        [TestMethod]
        [ExpectedException(typeof(BrowserException))]
        public void CheckIfTitleEquals_trim()
        {
            RunInAllBrowsers(browser =>
            {

                browser.NavigateToUrl();
                browser.CheckIfTitleEquals("This is title           ", trim: false);
            });
        }
        [TestMethod]
        [ExpectedException(typeof(BrowserException))]
        public void CheckIfTitleNotEquals_Exception()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleNotEquals("This is title");
            });
        }
        [TestMethod]
        [ExpectedException(typeof(BrowserException))]
        public void CheckIfTitleEquals_Exception()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleEquals("This is not title");
            });
        }
        [TestMethod]
        [ExpectedException(typeof(BrowserException))]
        public void CheckIfTitle_Exception()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitle(c=> c == "This is not title");
            });
        }
    }
}
