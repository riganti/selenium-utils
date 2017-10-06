using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class IsDisplayedTests : SeleniumTest
    {
        [TestMethod]
        public void IsDisplayed_CheckIfIsDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Displayed");
                browser.CheckIfIsDisplayed("#displayed");
                browser.First("#displayed").CheckIfIsDisplayed();
                browser.First("#displayed-zero-draw-rec").CheckIfIsDisplayed();
            });
        }

        [TestMethod]
        public void IsDisplayed_CheckIfIsNotDisplayed()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Displayed");
                browser.CheckIfIsNotDisplayed("#non-displayed");
                browser.First("#non-displayed").CheckIfIsNotDisplayed();
            });
        }

        [TestMethod]
        public void IsDisplayed_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Displayed");

                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#displayed").CheckIfIsNotDisplayed();
                });
                MSAssert.ThrowsException<UnexpectedElementStateException>(() =>
                {
                    browser.First("#non-displayed").CheckIfIsDisplayed();
                });
            });
        }
    }
}