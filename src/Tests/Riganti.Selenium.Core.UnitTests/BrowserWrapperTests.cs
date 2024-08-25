using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Selenium.Core.UnitTests.Mock;
using Selenium.Core.UnitTests;

namespace Riganti.Selenium.Core.UnitTests;

[TestClass]
public class BrowserWrapperTests : MockingTest
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public void CurrentUrlPath_NoEscapedCharactersTest()
    {
        var driverMock = new MockIWebDriver
        {
            FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "a" } },
            Url = "https://localhost:12345/path1/path2?query=1#fragment"
        };
        var browser = CreateMockedIBrowserWrapper(driverMock);
        Assert.AreEqual("/path1/path2?query=1#fragment", browser.CurrentUrlPath);
    }

    [TestMethod]
    public void CurrentUrlPath_WithEscapedCharatersTest()
    {
        var driverMock = new MockIWebDriver
        {
            FindElementsAction = () => new List<IWebElement>() { new MockIWebElement() { TagName = "a" } },
            Url = "https://localhost:12345/path1/path2%20second%20part?query=1#fragment"
        };
        var browser = CreateMockedIBrowserWrapper(driverMock);
        Assert.AreEqual("/path1/path2%20second%20part?query=1#fragment", browser.CurrentUrlPath);
    }
}
