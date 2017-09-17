using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IsCheckedTests : SeleniumTest
    {
        [TestMethod]
        public void IsNotCheckedTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox2").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void IsCheckedTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        public void IsCheckedApiTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#checkbox1"));
            });
        }
        
        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_TypeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#textbox1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TypeTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#textbox1").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_TagnameTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#span1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TagnameTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#span1").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void CheckIfIsChecked_RadioButton()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioChecked").CheckIfIsChecked();
            });
        }
        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_RadioButton_ExceptionExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioNotChecked").CheckIfIsChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_RadioButton()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioChecked").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_RadioButtonApi()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#RadioChecked"));
            });
        }

        [TestMethod]
        public void CheckIfIsNotChecked_RadioButton_ExceptionExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioNotChecked").CheckIfIsNotChecked();
            });
        }
        
        [TestMethod]
        public void CheckCheckboxes()
        {
            RunInAllBrowsers(browser =>
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