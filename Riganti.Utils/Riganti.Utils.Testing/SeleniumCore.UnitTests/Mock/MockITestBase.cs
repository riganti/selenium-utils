using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace SeleniumCore.UnitTests.Mock
{
    public class MockITestBase : ITestBase
    {
        public TestContext TestContext { get; set; }

        public void Log(Exception exception)
        {
        }

        public Guid ActiveScope { get; set; }
    }
}