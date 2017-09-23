﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace SeleniumCore.Samples.Tests
{

    [TestClass]
    public class TitleTests : SeleniumTest
    {
        [TestMethod]
        public void CheckIfTitleEquals()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleEquals("This is title       ");
            });
        }
        [TestMethod]
        public void CheckIfTitleNotEquals()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleNotEquals("This is not title            ");
            });
        }
        [TestMethod]
        public void CheckIfTitleEquals_trim()
        {
            this.RunInAllBrowsers(browser =>
            {

                browser.NavigateToUrl();

                Assert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitleEquals("This is title           ", trim: false);
                });
            });
        }
        [TestMethod]
        public void CheckIfTitleNotEquals_Exception()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Assert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitleNotEquals("This is title");
                });
            });
        }
        [TestMethod]
        public void CheckIfTitleEquals_Exception()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Assert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitleEquals("This is not title");
                });
            });
        }
        [TestMethod]
        public void CheckIfTitle_Exception()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Assert.ThrowsException<BrowserException>(() =>
                {
                    browser.CheckIfTitle(c => c == "This is not title");
                });
            });
        }
    }
}