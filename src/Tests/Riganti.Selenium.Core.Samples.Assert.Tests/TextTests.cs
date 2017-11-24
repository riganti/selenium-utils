using System.Collections.Generic;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.UnitTests.Mock;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{
    public class TextTests : AppSeleniumTest
    {
        public TextTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TextEmpty_ExpectedFailure()
        {
            var driver = new MockIWebDriver() { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { Text = "text" } } };
            var browser = CreateMockedIBrowserWrapper(driver);

            var div = browser.First("");
            Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.TextEmpty(div);
                });
        }
        [Fact]
        public void TextEmptyText()
        {
            var driver = new MockIWebDriver() { FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() } };
            var browser = CreateMockedIBrowserWrapper(driver);
            AssertUI.TextEmpty(browser.First("#empty-div"));
        }


    }

}