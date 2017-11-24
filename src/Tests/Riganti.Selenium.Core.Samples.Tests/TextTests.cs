using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    [TestClass]
    public class TextTests : AppSeleniumTest
    {
        [TestMethod]
        public void Text_CheckIfTextEquals()
        {
            RunInAllBrowsers(browser =>
           {
               browser.NavigateToUrl("/test/text");
               browser.First("#button").CheckIfTextEquals("text", false);
               browser.First("#input").CheckIfTextEquals("text", false);

               browser.First("#area").CheckIfTextEquals("TeXt", false);
               browser.First("#area").CheckIfTextEquals("text");
           });
        }

        [TestMethod]
        public void Text_CheckIfTextEquals_ExpectedFailure()
        {
            RunInAllBrowsers(browser =>
           {
               browser.NavigateToUrl("/test/text");
               Assert.ThrowsException<UnexpectedElementStateException>(() =>
               {
                   browser.First("#input").CheckIfTextEquals("text2", false);
               });
               Assert.ThrowsException<UnexpectedElementStateException>(() =>
               {
                   browser.First("#area").CheckIfTextEquals("TeXt");
               });
           });
        }

        [TestMethod]
        public void Text_CheckIfText()
        {
            RunInAllBrowsers(browser =>
           {
               browser.NavigateToUrl("/test/text");
               browser.First("#button").CheckIfText(s => s.ToLower().Contains("text"));
               browser.First("#input").CheckIfText(s => s.Contains("text"));
               browser.First("#area").CheckIfText(s => s.Contains("text"));
           });
        }

        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void Text_CheckIfText_ExpectedFailure()
        {
            RunInAllBrowsers(browser =>
           {
               browser.NavigateToUrl("/test/text");
               browser.First("#input").CheckIfText(s => !s.Contains("text"));
           });
        }
        [TestMethod]
        [ExpectedSeleniumException(typeof(UnexpectedElementStateException))]
        public void TextEmpty_ExpectedFailure()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
                var input = browser.First("#div-text");
                input.CheckIfTextEmpty();
            });
        }
        [TestMethod]
        public void TextEmptyText()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/text");
                browser.First("#empty-div").CheckIfTextEmpty();
            });
        }
    }
}