using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core
{
    public class TestContextProvider : ITestContextProvider
    {
        private TestContext context;

        public void SetContext(TestContext testContext)
        {
            context = testContext ?? throw new ArgumentNullException(nameof(testContext));
        }

        public ITestInstanceContext CreateTestContext(TestInstance testInstance)
        {
            if (testInstance == null) throw new ArgumentNullException(nameof(testInstance));
            if (context == null)
            {
                throw new InvalidOperationException("TestContext is not set.");
            }

            return new TestInstanceContextWrapper(context, testInstance);

        }

        public ITestContext GetGlobalScopeTestContext() => new TestContextWrapper(context);
    }
}