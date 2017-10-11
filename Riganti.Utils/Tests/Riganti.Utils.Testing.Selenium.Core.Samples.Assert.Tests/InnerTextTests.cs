using Riganti.Utils.Testing.Selenium.AssertApi;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.AssertApi.Tests
{
    public class InnerTextTests : AppSeleniumTest
    {
        public InnerTextTests(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void InnerTextOfFirstElement()
        {
            RunInAllBrowsers(browser =>
            {
                Assert.InnerText(browser.First("*"), s => s.Contains("red"));
            });
        }
    }
}