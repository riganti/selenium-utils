﻿using System;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core
{
    public class TestContextProvider : ITestContextProvider
    {
        private ITestOutputHelper outputHelper;
        private ITestContext testContext;

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
            if (testContext == null)
            {
                testContext = new TestContextWrapper(outputHelper); //TODO
            }

            return testContext;
        }
    }

   
}