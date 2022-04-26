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