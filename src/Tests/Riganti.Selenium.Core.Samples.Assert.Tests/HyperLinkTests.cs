using System;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{

    public class HyperLinkTests : AppSeleniumTest
    {
        [Fact]
        public void Url_CheckHyperLink_Absolute()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");

                AssertUI.HyperLinkEquals(browser.First("#AbsoluteLink"), "https://www.google.com/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.PathAndQuery);
                AssertUI.HyperLinkEquals(browser.First("#AbsoluteSameSchema"), "https://localhost:1234/path/test?query=test#fragment", UrlKind.Absolute, UriComponents.NormalizedHost | UriComponents.PathAndQuery);
                AssertUI.HyperLinkEquals(browser.First("#AbsoluteLink2"), "https://localhost:1234/path/test?query2=test2&query=test#fragment", UrlKind.Absolute, false, UriComponents.NormalizedHost | UriComponents.PathAndQuery);
            });
        }
        [Fact]
        public void Url_CheckHyperLink_Absolute_ExceptionExpected()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/HyperLink");
                Assert.Throws<UnexpectedElementStateException>(() =>
                {
                    AssertUI.HyperLinkEquals(browser.First("#AbsoluteLink2"),
                        "https://localhost:1234/path/test?query2=test2&query=test#fragment", UrlKind.Absolute,
                        true, UriComponents.NormalizedHost | UriComponents.PathAndQuery);
                });
            });
        }

        public HyperLinkTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
