using System;
using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using Selenium.Core.UnitTests.Mock;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class TimeOutTests
    {
        #region Browser tests

        [TestMethod, Timeout(5000), ExpectedException(typeof(WaitBlockException), AllowDerivedTypes = true)]
        public void BrowserWaitForTimeoutTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            browser.WaitFor(() => false, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000), ExpectedException(typeof(WaitBlockException), AllowDerivedTypes = true)]
        public void BrowserWaitForTimeoutTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var i = 0;
            browser.WaitFor(() => i++ == 5, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000)]
        public void BrowserWaitForTimeoutTest3()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var i = 0;
            browser.WaitFor(() => i++ == 5, 2000, "test timeouted", checkInterval: 100);
        }

        [TestMethod, Timeout(5000), ExpectedException(typeof(WaitBlockException), AllowDerivedTypes = true)]
        public void BrowserWaitForTimeoutTest5()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());

            browser.WaitFor(new Action(() =>
            {
                throw new Exception("Condition is not valid.");
            }), 2000, "test timeouted", checkInterval: 100);
        }
        [TestMethod, Timeout(5000)]
        public void BrowserWaitForTimeoutTest6()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var i = 0;
            browser.WaitFor(() =>
            {

                if (i++ == 3)
                {
                    return;
                }
                throw new Exception("Not valid.");
            }, 2000, "test timeouted", checkInterval: 100);
        }
        #endregion Browser tests

        #region Element tests

        [TestMethod, Timeout(5000), ExpectedException(typeof(WaitBlockException), AllowDerivedTypes = true)]
        public void ElementWaitForTimeoutTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            element.WaitFor(elm => false, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000), ExpectedException(typeof(WaitBlockException), AllowDerivedTypes = true)]
        public void ElementWaitForTimeoutTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            var i = 0;
            element.WaitFor(elm => i++ == 5, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000)]
        public void ElementWaitForTimeoutTest4()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            var i = 0;
            element.WaitFor(elm => i++ == 5, 2000, "test timeouted", checkInterval: 100);
        }

        [TestMethod, Timeout(5000), ExpectedException(typeof(WaitBlockException), AllowDerivedTypes = true)]
        public void ElementWaitForTimeoutTest5()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            var i = 0;
            element.WaitFor((elm) => elm.CheckIfValue("asdasdasdasd"), 2000, "test timeouted", checkInterval: 100);
        }
        [TestMethod, Timeout(5000)]
        public void ElementWaitForTimeoutTest6()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockISeleniumTest(), new ScopeOptions());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            var i = 0;
            element.WaitFor((elm) =>
            {
                
                if (i++ == 3)
                {
                    return;    
                }
                throw new Exception("Not valid.");
            }, 2000, "test timeouted", checkInterval: 100);
        }
        #endregion Element tests
    }
}