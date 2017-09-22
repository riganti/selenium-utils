using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IsDisplayedTests : SeleniumTest
    {
        [TestMethod]
        public void CheckIfIsDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.Wait();
                browser.CheckIfIsDisplayed("#displayed");
                browser.First("#displayed").CheckIfIsDisplayed();
            });
        }
        [TestMethod]
        public void CheckIfIsDisplayed_ExceptionCheck()
        {
            try
            {
                this.RunInAllBrowsers(browser =>
                {
                    browser.NavigateToUrl();
                    browser.CheckIfIsDisplayed("#non-displayed");
                });
                throw new TestFrameworkWrongBehaviorException("The element is not visible and test framework does not reacted corectly.");
            }
            catch (Exception e)
            {
                if (e is  TestFrameworkWrongBehaviorException)
                {
                    throw;
                } 
            }
        }

        [TestMethod]
        public void CheckIfIsNotDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfIsNotDisplayed("#non-displayed");
                browser.First("#non-displayed").CheckIfIsNotDisplayed();
                browser.First("#displayed-zero-draw-rec").CheckIfIsDisplayed();
            });
        }
    }
}