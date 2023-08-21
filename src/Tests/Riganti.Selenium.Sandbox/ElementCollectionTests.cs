using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Sandbox
{
    public class ElementCollectionTests : AppSeleniumTest
    {
        public ElementCollectionTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TestBackingReferencesFromCollection()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                var a = browser.FindElements("div");
                var b = a.FindElements("p");
                var c = b.BrowserWrapper;
                var d = c.FindElements("div");
                var e = d.FindElements("p");
                e.ThrowIfSequenceEmpty();
            });
        }

        [Fact]
        public void TestBackingReferencesFromCollection_WithExplicitSelector()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                var a = browser.FindElements("innerDiv1", s => By.CssSelector($"[data-ui={s}]"));
                var b = a.FindElements("p");
                b.ThrowIfDifferentCountThan(3);
            });
        }
    }
}
