using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.Tests
{
    [TestClass]
    public class TestContextTests : SeleniumTest
    {
        [TestMethod]
        public void TestContextConversionTest()
        {
            Assert.IsNotNull(Context.TestName);
        }
    }
}