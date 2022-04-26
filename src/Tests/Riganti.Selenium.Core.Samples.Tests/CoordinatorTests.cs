using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions.Attributes;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    [FullStackTrace]
    public class CoordinatorTests : SeleniumTest
    {
        [TestMethod]
        public void Coordinator_Google()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("https://google.com");
                browser.CheckIfTitleEquals("Google");
            });
        }
        [TestMethod]
        public void Coordinator_Google2()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("https://google.com");
                browser.CheckIfTitleEquals("Google");
            });
        }
        [TestMethod]
        public void Coordinator_Google3()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("https://google.com");
                browser.CheckIfTitleEquals("Google");
            });
        }
        [TestMethod]
        public void Coordinator_Google4()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("https://google.com");
                browser.CheckIfTitleEquals("Google");
            });
        }
    }
}
