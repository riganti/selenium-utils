using System;
using System.Net.NetworkInformation;
using System.Threading;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TestContextAccessor
    {

        private readonly ThreadLocal<ITestInstanceContext> testContextStore = new ThreadLocal<ITestInstanceContext>();

        
        public ITestInstanceContext GetTestContext()
        {
            return testContextStore.Value;
        }

        public IDisposable Scope(ITestInstanceContext testInstanceContext)
        {
            testContextStore.Value = testInstanceContext;
            return new TestContextScope(this);
        }


        class TestContextScope : IDisposable
        {
            private readonly TestContextAccessor accessor;

            public TestContextScope(TestContextAccessor accessor)
            {
                this.accessor = accessor;
            }

            public void Dispose()
            {
                accessor.testContextStore.Value = null;
            }
        }
    }
}