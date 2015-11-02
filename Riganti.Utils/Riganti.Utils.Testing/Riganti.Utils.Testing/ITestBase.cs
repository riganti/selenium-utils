using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface ITestBase
    {
        TestContext TestContext { get; set; }
        void Log(Exception exception);
    }
}