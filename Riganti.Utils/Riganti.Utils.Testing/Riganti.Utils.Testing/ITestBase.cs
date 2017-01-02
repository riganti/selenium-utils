using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestBase
    {
        ITestContext TestContext { get; set; }
        void Log(Exception exception);
        Guid ActiveScope { get; set; }
    }
}