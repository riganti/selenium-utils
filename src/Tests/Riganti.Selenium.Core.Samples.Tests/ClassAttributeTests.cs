using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Samples.FluentApi.Tests;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    [TestClass]
    public class ClassAttributeTests : AppSeleniumTest
    {
        public const string TestPageUrl = "/test/CssClasses";

        [TestMethod]
        public void CssStyle_CssStyleValueEquals()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                var elm = browser.First("#hasClasses");
                elm.CheckClassAttribute("class1 class2");
                elm.CheckClassAttribute(new[] { "class1", "class2"});
                elm.CheckClassAttribute(new[] { "class88", "class2"});
            });
        }
    }
}