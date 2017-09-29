using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class IFrameTests : SeleniumTest
    {
        [TestMethod]
        public void IFrame_CheckInsideIFrame()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FrameTest1");
                browser.First("#iframe-test");

                var frame = browser.GetFrameScope("#iframe-test");
                frame.First("#frame2-text").CheckIfTextEquals("frame2 text");
            });
        }
        [TestMethod]
        public void IFrame_CheckAllView()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FrameTest1");
                var frame = browser.GetFrameScope("#iframe-test");
                frame.First("#frame2-text");

                var elm = browser.First("#outside-iframe");
                elm.First("#child").CheckIfTextEquals("child");
            });
        }
        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void IFrame_GetFrameScope_ExceptionExpected()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FrameTest1");
                var frame = browser.GetFrameScope("#outside-iframe");
            });
        }
    }
}