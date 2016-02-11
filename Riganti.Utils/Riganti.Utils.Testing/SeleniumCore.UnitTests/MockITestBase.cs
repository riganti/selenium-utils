using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace SeleniumCore.UnitTests
{
    public class MockITestBase : ITestBase
    {
        public TestContext TestContext { get; set; }

        public void Log(Exception exception)
        {
        }
    }
}