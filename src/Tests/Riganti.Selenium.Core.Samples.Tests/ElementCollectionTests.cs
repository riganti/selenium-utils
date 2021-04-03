using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class ElementCollectionTests : AppSeleniumTest
    {
        [TestMethod]
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
      

    }
}