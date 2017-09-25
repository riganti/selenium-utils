using System;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.xUnitIntegration;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
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