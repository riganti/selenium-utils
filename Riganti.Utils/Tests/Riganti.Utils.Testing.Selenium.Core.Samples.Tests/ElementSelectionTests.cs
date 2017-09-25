using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;

namespace SeleniumCore.Samples.Tests
{
    [TestClass]
    public class ElementSelectionTests : SeleniumTest
    {
        [TestMethod]
        public void ElementChildrenSelection()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/ElementSelectionTest");

                var elm = browser.First("#top-element");
                Assert.AreEqual(elm.Children.Count, 4);
            });
        }
    }
}