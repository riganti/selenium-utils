using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Samples.FluentApi.Tests;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    [TestClass]
    public class CssStylesTests : AppSeleniumTest
    {
        public const string TestPageUrl = "/test/CssStyles";

        [TestMethod]
        public void CssStyle_CssStyleValueEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);

                browser.First("#hasStyles")
                    .CheckCssStyle("font-size", "8px")
                    .CheckCssStyle("width", "20px")
                    .CheckCssStyle("height", "20px");

                browser.First("#hasNotStyles")
                    .CheckCssStyle("margin-left", "8px");
            });
        }
    }
}
