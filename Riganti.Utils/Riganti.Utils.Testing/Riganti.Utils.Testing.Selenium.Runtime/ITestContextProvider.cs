namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public interface ITestContextProvider
    {
        ITestContext CreateTestContext(TestConfiguration testConfiguration);
    }
    
}