using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests
{
    [TestClass]
    public class IFrameTests : SeleniumTest
    {
        [TestMethod]
        public void IFrame_CheckInsideIFrame()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/IFrameMain");
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
                browser.NavigateToUrl("/test/IFrameMain");
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
                browser.NavigateToUrl("/test/IFrameMain");
                var frame = browser.GetFrameScope("#outside-iframe");
            });
        }
    }
}