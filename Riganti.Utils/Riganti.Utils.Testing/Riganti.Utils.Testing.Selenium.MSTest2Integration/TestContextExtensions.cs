using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public static class TestContextExtensions
    {
        public static ITestContext Wrap(this TestContext context)
        {
            return new TestContextWrapper(context);
        }
    }
}