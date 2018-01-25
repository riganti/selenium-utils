using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class ClassAttributeTests : AppSeleniumTest
    {
        public const string TestPageUrl = "/test/CssClasses";
        public ClassAttributeTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void HasClass_ProvidedMoreThenOne()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                AssertUI.HasClass(browser.First("#hasClasses"), "class1");
                AssertUI.HasClass(browser.First("#hasClasses"), "class2");
                AssertUI.HasClass(browser.First("#hasClasses"), "class3");
                AssertUI.HasClass(browser.First("#hasClasses"), "class4");
                AssertUI.HasClass(browser.First("#hasClasses"), "class1 class4");
                AssertUI.HasClass(browser.First("#hasClasses"), "class1   class4");
            });
        }
        [Fact]
        public void HasClass_ProvidedMoreThenOne_FailureExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HasClass(browser.First("#hasClasses"), "class6");
                });
            });
        }
        [Fact]
        public void HasClass_ProvidedMoreThenOne2_FailureExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HasClass(browser.First("#hasClasses"), "class6 class4");
                });
            });
        }
        [Fact]
        public void HasClass_ProvidedMoreThenOne3_FailureExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HasClass(browser.First("#hasClasses"), "class6 class4     ");
                });
            });
        }
        [Fact]
        public void HasClass_ProvidedEmpty()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                AssertUI.HasClass(browser.First("#emptyClasses"), "");
            });
        }
        [Fact]
        public void HasClass_ProvidedEmpty_FailureExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HasClass(browser.First("#emptyClasses"), "class0");
                });
            });
        }
        [Fact]
        public void HasClass_NoProvidedClasses()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);

                AssertUI.HasClass(browser.First("#hasNotClasses"), "");
            });
        }



        [Fact]
        public void HasNotClass_ProvidedMoreThenOne()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                AssertUI.HasNotClass(browser.First("#hasClasses"), "class0");
            });
        }
        [Fact]
        public void HasNotClass_ProvidedMoreThenOne_FailureExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HasNotClass(browser.First("#hasClasses"), "class1");
                });
            });
        }

        [Fact]
        public void HasNotClass_ProvidedEmpty()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);

                AssertUI.HasNotClass(browser.First("#emptyClasses"), "class1");
            });
        }
        [Fact]
        public void HasNotClass_ProvidedEmpty_FailureExpected()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HasNotClass(browser.First("#emptyClasses"), "");
                });
            });
        }
        [Fact]
        public void HasNotClass_NoProvidedClasses()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(TestPageUrl);

                AssertUI.HasNotClass(browser.First("#hasNotClasses"), "class0");
            });
        }

    }
}
