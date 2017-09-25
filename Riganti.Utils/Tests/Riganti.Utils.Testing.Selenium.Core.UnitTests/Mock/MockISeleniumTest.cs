using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockISeleniumTest : ISeleniumTest
    {
        public ITestInstanceContext InstanceContext { get; set; }
        
        public Guid CurrentScope { get; set; }
        public TestSuiteRunner TestSuiteRunner { get; }
    }
}