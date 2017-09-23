using Riganti.Utils.Testing.Selenium.AssertApi;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.AssertApi.Tests
{
    class InnerTextTests : AppSeleniumTest
    {
        public InnerTextTests(ITestOutputHelper output) : base(output)
        {
            RunInAllBrowsers(browser =>
            {
               Assert.CheckIfAlertText(browser, s=> s.Contains("red"));
            });
        }
    }
}