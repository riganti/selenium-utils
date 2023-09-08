using System;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Sandbox
{
    public class CssStylesTests : AppSeleniumTest
    {
        public const string TestPageUrl = "/test/CssStyles";

        public CssStylesTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
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
