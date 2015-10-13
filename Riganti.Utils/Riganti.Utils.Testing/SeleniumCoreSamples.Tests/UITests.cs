using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.SeleniumCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Tests
{
    [TestClass]
    public class UiTests : SeleniumTestBase
    {
        [TestMethod]
        public void CheckIfIsDisplayed()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsDisplayed("#displayed");
                browser.First("#displayed").CheckIfIsDisplayed();
            });
        }

        [TestMethod]
        public void CheckIfIsNotDisplayed()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Thread.Sleep(3000);
                browser.CheckIfIsNotDisplayed("#non-displayed");
                browser.First("#non-displayed").CheckIfIsNotDisplayed();
            });
        }

        [TestMethod]
        public void GetFullSelector()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                TestContext.WriteLine(
                    browser.First("#displayed").FindElements("div p")
                    .FullSelector);
            });
        }
    }
}