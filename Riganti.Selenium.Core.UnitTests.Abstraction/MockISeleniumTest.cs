using System;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core.UnitTests.Mock
{
    public class MockISeleniumTest : ISeleniumTest
    {
        public ITestInstanceContext InstanceContext { get; set; }
        
        public Guid CurrentScope { get; set; }
        public TestSuiteRunner TestSuiteRunner { get; set; }
    }
}