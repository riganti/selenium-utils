namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TestContextProvider : ITestContextProvider
    {
        public ITestContext CreateTestContext(TestInstance testInstance)
        {
            var context = ((SeleniumTest) testInstance.TestClass).TestContext;
            return new TestContextWrapper(context, testInstance);
        }
    }
}