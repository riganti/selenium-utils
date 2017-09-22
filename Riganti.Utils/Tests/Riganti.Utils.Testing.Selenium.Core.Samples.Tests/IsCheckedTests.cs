using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IsCheckedTests : SeleniumTest
    {
        [TestMethod]
        public void IsNotCheckedTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox2").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void IsCheckedTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        public void IsCheckedApiTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_TypeTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#textbox1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TypeTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#textbox1").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_TagnameTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#span1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TagnameTest()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#span1").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void CheckIfIsChecked_RadioButton()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioChecked").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_RadioButton_ExceptionExpected()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioNotChecked").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_RadioButton()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioChecked").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_RadioButtonApi()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioChecked").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void CheckIfIsNotChecked_RadioButton_ExceptionExpected()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioNotChecked").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void CheckCheckboxes()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Checkboxes.aspx");
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