using System;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.xUnitIntegration;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core
{
    public class TestContextProvider : ITestContextProvider
    {
        private ITestOutputHelper outputHelper;
        public void SetContext(ITestOutputHelper helper)
        {
            outputHelper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public ITestInstanceContext CreateTestContext(TestInstance testInstance)
        {
            if (testInstance == null) throw new ArgumentNullException(nameof(testInstance));
            if (outputHelper == null)
            {
                throw new InvalidOperationException("TestContext is not set.");
            }
            return new TestInstanceContextWrapper(outputHelper, testInstance);
        }

        public ITestContext GetGlobalScopeTestContext()
        {
            return new TestContextWrapper(outputHelper);
        }
    }

   
}