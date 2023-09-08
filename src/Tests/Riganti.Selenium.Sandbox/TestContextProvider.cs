using System;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Sandbox
{
    public class TestContextProvider : ITestContextProvider
    {

        public ITestInstanceContext CreateTestContext(TestInstance testInstance)
        {
            return new TestContextWrapper(testInstance);

        }

        public ITestContext GetGlobalScopeTestContext() => new TestContextWrapper(null);
    }
}
