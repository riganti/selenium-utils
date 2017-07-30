using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public class TestContextAccessor
    {

        private readonly ThreadLocal<ITestContext> testContextStore = new ThreadLocal<ITestContext>();

        
        public ITestContext GetTestContext()
        {
            return testContextStore.Value;
        }

        public IDisposable Scope(ITestContext testContext)
        {
            testContextStore.Value = testContext;
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