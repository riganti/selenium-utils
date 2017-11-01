using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestContextProvider

    {
        ITestInstanceContext CreateTestContext(TestInstance testInstance);
        ITestContext GetGlobalScopeTestContext();
    }

}