using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class IsCheckedTests : SeleniumTest
    {
        [TestMethod]
        public void IsChecked_CheckIfIsNotChecked()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                browser.Single("#checkbox2").CheckIfIsNotChecked();
                browser.Single("#RadioNotChecked").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void IsChecked_CheckIfIsChecked()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                browser.Single("#checkbox1").CheckIfIsChecked();
                browser.Single("#RadioChecked").CheckIfIsChecked();
            });
        }

        [TestMethod]
        public void IsChecked_CheckIfIsChecked_TypeFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                MSAssert.ThrowsException<UnexpectedElementStateException>((() =>
                {
                    browser.Single("#textbox1").CheckIfIsChecked();
                }));
                MSAssert.ThrowsException<UnexpectedElementStateException>((() =>
                {
                    browser.Single("#span1").CheckIfIsChecked();
                }));
            });
        }

        [TestMethod]
        public void IsChecked_CheckIfIsNotChecked_TypeFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                MSAssert.ThrowsException<UnexpectedElementStateException>((() =>
                {
                    browser.Single("#textbox1").CheckIfIsNotChecked();
                }));
                MSAssert.ThrowsException<UnexpectedElementStateException>((() =>
                {
                    browser.Single("#span1").CheckIfIsNotChecked();
                }));
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void IsChecked_CheckIfIsChecked_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                browser.Single("#RadioNotChecked").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void IsChecked_CheckIfIsNotChecked_ExpectedFailure()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                browser.Single("#RadioChecked").CheckIfIsNotChecked();
            });
        }



        [TestMethod]
        public void IsChecked_CheckStateSwitching()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Checkboxes");
                browser.First("#checkbox1").Wait(1200)
                                           .CheckIfIsChecked()
                                           .Wait(1200)
                                           .Click()
                                           .Wait(1200)
                                           .CheckIfIsNotChecked();

                browser.First("#checkbox2").CheckIfIsNotChecked()
                                            .Wait(1200)
                                            .Click()
                                            .Wait(1200)
                                            .CheckIfIsChecked();
            });
        }
    }
}