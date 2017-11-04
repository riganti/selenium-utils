using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Api;
using Riganti.Selenium.Core.Exceptions;
using Selenium.Core.UnitTests.Mock;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class BrowserWrapperTestsNewApi
    {
        [TestMethod]
        public void ExactUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrlEquals(browser, "https://localhost:12345/path1/path2?query=1#fragment");
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void ExactUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrlEquals(browser, "https://localhost:12345/spath1/path2?query=1#fragment");
        }

        [TestMethod]
        public void HostUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "https://localhost:12345/", UrlKind.Absolute, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void HostUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "https://localhosst:12345/", UrlKind.Absolute, UriComponents.Host);
        }

        [TestMethod]
        public void PathUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "/path1/path2", UrlKind.Relative, UriComponents.Path);
        }

        [TestMethod]
        public void PathUrlMatchTest3()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "https://localhost:12345/path1/path2", UrlKind.Absolute, UriComponents.Path);
        }

        [ExpectedException(typeof(BrowserLocationException))]
        [TestMethod]
        public void PathUrlMatchTest4()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "https://localhost:12345/path1/path2/nonon", UrlKind.Absolute, UriComponents.Path);
        }

        [TestMethod]
        public void PathUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "/path1/path2", UrlKind.Relative, UriComponents.Path);
        }

        [TestMethod]
        public void PathAndHostUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost/path1/path2", UrlKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PathAndHostUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost/path10/path2", UrlKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PathAndHostUrlMatchTest4()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhosta/path1/path2", UrlKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PathAndHostUrlMatchTest3()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost0/path10/path2", UrlKind.Absolute, UriComponents.Path, UriComponents.Host);
        }

        [TestMethod]
        public void PathHostAndPortUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost:12345/path1/path2", UrlKind.Absolute, UriComponents.Path, UriComponents.HostAndPort);
        }

        [TestMethod]
        public void QueryUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost:12345/path1/path2?query=1", UrlKind.Relative, UriComponents.Query);
        }

        [TestMethod]
        public void QueryUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "?query=1", UrlKind.Relative, UriComponents.Query);
        }

        [TestMethod]
        public void FragmentUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost:12345/path1/path2#fragment", UrlKind.Absolute, UriComponents.Fragment);
        }

        [TestMethod]
        public void FragmentUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "#fragment", UrlKind.Relative, UriComponents.Fragment);
        }

        [TestMethod]
        public void SchemaUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "https://ex.com/", UrlKind.Absolute, UriComponents.Scheme);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void SchemaUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "http://ex.com/", UrlKind.Absolute, UriComponents.Scheme);
        }

        [TestMethod]
        public void PortUrlMatchTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "//localhost:12345", UrlKind.Absolute, UriComponents.Port);
        }

        [TestMethod]
        [ExpectedException(typeof(BrowserLocationException))]
        public void PortUrlMatchTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            AssertUI.CheckUrl(browser, "https://ex.com/", UrlKind.Absolute, UriComponents.Port);
        }

        [TestMethod]
        public void CompareUrlTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            Assert.IsTrue(browser.CompareUrl("//localhost:12345", UrlKind.Absolute, UriComponents.Port));
        }
    }
}