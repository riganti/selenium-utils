using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using Selenium.Core.UnitTests.Mock;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class TimeOutTests
    {
        [TestMethod, Timeout(5000), ExpectedException(typeof(SeleniumTestFailedException))]
        public void BrowserWaitForTimeoutTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase(), new ScopeOptions());
            browser.WaitFor(() => false, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000), ExpectedException(typeof(SeleniumTestFailedException))]
        public void ElementWaitForTimeoutTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            element.WaitFor(elm => false, 2000, "test timeouted");
        }


        [TestMethod, Timeout(5000)]
        public void BrowserWaitForTimeoutTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase(), new ScopeOptions());
            var i = 0;
            browser.WaitFor(() => i++ == 5, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000)]
        public void ElementWaitForTimeoutTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            var i = 0;
            element.WaitFor(elm => i++ == 5, 2000, "test timeouted");
        }
       
    }
}