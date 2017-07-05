using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IsCheckedTestsNewApi : SeleniumTest
    {
        [TestMethod]
        public void IsNotCheckedTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#checkbox2"));
            });
        }

        [TestMethod]
        public void IsCheckedTest()
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#textbox1"));
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TypeTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#textbox1"));
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_TagnameTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#span1"));
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TagnameTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#span1"));
            });
        }

        [TestMethod]
        public void CheckIfIsChecked_RadioButton()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#RadioChecked"));
            });
        }
        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_RadioButton_ExceptionExpected()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#RadioNotChecked"));
            });
        }



        [TestMethod]
        [ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_RadioButton()
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#RadioNotChecked"));
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