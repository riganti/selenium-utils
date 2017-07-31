using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockISeleniumTest : ISeleniumTest
    {
        public ITestContext Context { get; set; }
        
        public Guid CurrentScope { get; set; }
    }
}