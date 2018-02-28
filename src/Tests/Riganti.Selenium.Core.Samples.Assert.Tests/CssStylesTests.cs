using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
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
                AssertUI.CssStyle(browser.First("#hasStyles"), "font-size", "8px");
                AssertUI.CssStyle(browser.First("#hasStyles"), "width", "20px");
                AssertUI.CssStyle(browser.First("#hasStyles"), "height", "20px");
                AssertUI.CssStyle(browser.First("#hasNotStyles"), "margin-left", "8px");
            });
        }
    }
}
