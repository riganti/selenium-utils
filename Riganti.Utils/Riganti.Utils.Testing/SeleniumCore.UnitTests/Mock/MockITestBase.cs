using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;

namespace Selenium.Core.UnitTests.Mock
{
    public class MockITestBase : ITestBase
    {
        public ITestContext Context { get; set; }

        public void Log(Exception exception)
        {
        }

        public Guid ActiveScope { get; set; }
    }
}