using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.xUnitIntegration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TestContextProvider : ITestContextProvider
    {
        public ITestContext CreateTestContext(TestInstance testInstance)
        {
            var context = ((SeleniumTest)testInstance.TestClass).TestOutput;
            return new TestContextWrapper(context, testInstance);
        }
    }
}