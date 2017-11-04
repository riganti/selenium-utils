using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core
{
    public interface ITestContextProvider

    {
        ITestInstanceContext CreateTestContext(TestInstance testInstance);
        ITestContext GetGlobalScopeTestContext();
    }

}