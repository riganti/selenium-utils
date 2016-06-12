using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestBase
    {
        TestContext TestContext { get; set; }
        void Log(Exception exception);
        Guid ActiveScope { get; set; }
    }
}