using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Api;
using Riganti.Selenium.Core.Exceptions;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IsCheckedTestsNewApi : SeleniumTest
    {
        [TestMethod]
        public void IsNotCheckedTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#checkbox2"));
            });
        }

        [TestMethod]
        public void IsCheckedTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#checkbox1"));
            });
        }

        [TestMethod]
        //[ExpectedException(typeof(UnexpectedElementStateException))]
        public void heckIfIsChecked_TypeTest()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#textbox1"));
            });
        }

        [TestMethod]
        //[ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TypeTest()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#textbox1"));
            });
        }

        [TestMethod]
        //[ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_TagnameTest()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#span1"));
            });
        }

        [TestMethod]
        //[ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_TagnameTest()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsNotChecked(browser.Single("#span1"));
            });
        }

        [TestMethod]
        public void CheckIfIsChecked_RadioButton()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#RadioChecked"));
            });
        }
        [TestMethod]
        //[ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsChecked_RadioButton_ExceptionExpected()
        {
            ExpectException(typeof(UnexpectedElementStateException));
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("Checkboxes.aspx");
                AssertUI.CheckIfIsChecked(browser.Single("#RadioNotChecked"));
            });
        }

        [TestMethod]
        //[ExpectedException(typeof(UnexpectedElementStateException))]
        public void CheckIfIsNotChecked_RadioButton()
        {
            ExpectException(typeof(UnexpectedElementStateException));
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
                AssertUI.CheckIfIsNotChecked(browser.Single("#RadioNotChecked"));
            });
        }

        [TestMethod]
        public void CheckCheckboxes()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Checkboxes.aspx");
                var element = browser.First("#checkbox1").Wait(1200)
                    .CheckIfIsChecked()
                    .Wait(1200)
                    .Click()
                    .Wait(1200);
                AssertUI.CheckIfIsNotChecked(element);

                element = browser.First("#checkbox2").CheckIfIsNotChecked()
                    .Wait(1200)
                    .Click()
                    .Wait(1200);

                AssertUI.CheckIfIsChecked(element);
            });
        }
    }
}