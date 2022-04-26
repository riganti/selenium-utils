using System;
using System.Threading;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core
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