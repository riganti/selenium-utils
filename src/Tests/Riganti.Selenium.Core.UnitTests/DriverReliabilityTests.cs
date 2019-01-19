using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace Riganti.Selenium.Core.UnitTests
{
    [TestClass]
    public class DriverReliabilityTests
    {
        [TestMethod, Ignore]
        public void BrowserDropTest()
        {
            var driver = new ChromeDriver();
        }
    }
}