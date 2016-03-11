using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;

namespace WebApplication1.Tests
{
    [TestClass]
    public class IsCheckedTests : SeleniumTestBase
    {
        [TestMethod]
        public void IsNotCheckedTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox2").CheckIfIsNotChecked();
            });
        }

        [TestMethod]
        public void IsCheckedTest()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#checkbox1").CheckIfIsChecked();
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
                browser.Single("#textbox1").CheckIfIsChecked();
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
                browser.Single("#textbox1").CheckIfIsNotChecked();
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
                browser.Single("#span1").CheckIfIsChecked();
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
                browser.Single("#span1").CheckIfIsChecked();
            });
        }

        [TestMethod]
        public void CheckIfIsChecked_RadioButton()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
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
            SeleniumTestsConfiguration.DeveloperMode = true;
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                browser.Single("#RadioChecked").CheckIfIsNotChecked();
            });
        }
        [TestMethod]
        public void CheckIfIsNotChecked_RadioButton_ExceptionExpected()
        {
            SeleniumTestsConfiguration.DeveloperMode = true;
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