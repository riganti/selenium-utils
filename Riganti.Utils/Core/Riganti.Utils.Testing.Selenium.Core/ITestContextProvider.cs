using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestContextProvider

    {
        ITestContext CreateTestContext(TestInstance testInstance);
    }

}