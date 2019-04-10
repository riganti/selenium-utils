using System;
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

        [TestMethod]
        public void CssStyle_CssStyleValueEquals_ExceptionExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                var elm = browser.First("#hasClasses");
                try
                {
                    elm.CheckClassAttribute("sclass1 class2s");
                }
                catch (Exception e)
                {
                    if (!(e.Message.Contains("sclass1") && e.Message.Contains("class2s")))
                    {
                        throw new Exception("Exception message does not contain 'sclass1' and 'class2s'.");
                    }
                }
            });
        }
    }
}