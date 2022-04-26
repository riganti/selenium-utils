using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class ElementReloadTests : AppSeleniumTest
    {
        public ElementReloadTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ReloadElement");

                var inputElm =
                browser
                    .First("#div1")
                    .Last("#div2")
                    .Single("#div3")
                    .FirstOrDefault("input");
                AssertUI.TextEquals(inputElm, "init");
                inputElm.Click();
                AssertUI.Value(inputElm, "changed");

            });
        }
    }
}