using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;

namespace SeleniumCore.UnitTests
{
    [TestClass]
    public class TimeOutTests
    {
        [TestMethod, Timeout(5000), ExpectedException(typeof(SeleniumTestFailedException))]
        public void BrowserWaitForTimeoutTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            browser.WaitFor(() => false, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000), ExpectedException(typeof(SeleniumTestFailedException))]
        public void ElementWaitForTimeoutTest()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            element.WaitFor(elm => false, 2000, "test timeouted");
        }


        [TestMethod, Timeout(5000)]
        public void BrowserWaitForTimeoutTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            var i = 0;
            browser.WaitFor(() => i++ == 5, 2000, "test timeouted");
        }

        [TestMethod, Timeout(5000)]
        public void ElementWaitForTimeoutTest2()
        {
            var browser = new BrowserWrapper(new MockIWebDriver(), new MockITestBase());
            var element = new ElementWrapper(new MockIWebElement(), browser);
            var i = 0;
            element.WaitFor(elm => i++ == 5, 2000, "test timeouted");
        }



    }
}