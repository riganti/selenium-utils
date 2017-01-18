using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestBase
    {
        ITestContext Context { get; set; }
        void Log(Exception exception);
        Guid ActiveScope { get; set; }
    }
}