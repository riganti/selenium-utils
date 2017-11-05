using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.Core.UnitTests
{
    [TestClass]
    public class ExceptionsTests
    {
        private const string UrlConst = "https://example.com/";

        public TestContext TestContext { get; set; }
        [TestMethod]
        public void SeleniumTestFailedExceptionTest()

        {
            var exps = new List<Exception>() { new UnexpectedElementException("Exception1"){CurrentUrl = UrlConst, WebBrowser = "Chrome"}, new UnexpectedElementException("Exception2"), new UnexpectedElementException("Exception3") };
            var exp = new SeleniumTestFailedException(exps);
            var str = exp.ToString();

            TestContext.WriteLine(str);

            Assert.IsTrue(str.Contains("Exception1"));
            Assert.IsTrue(str.Contains("Exception2"));
            Assert.IsTrue(str.Contains("Exception3"));
            Assert.IsTrue(str.Contains("Url: https://example.com"));
            Assert.IsTrue(str.Contains("Browser: Chrome"));
            Assert.IsTrue(str.Contains(UrlConst));
        }

    }

}