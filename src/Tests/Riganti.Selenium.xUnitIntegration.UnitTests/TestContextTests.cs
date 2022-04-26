using System.IO;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.xUnitIntegration.UnitTests
{
    public class TestContextTests
    {
        private readonly ITestOutputHelper context;

        public TestContextTests(ITestOutputHelper context)
        {
            this.context = context;
        }
        [Fact]
        public void AddResultFileTest()
        {
            var a = new TestContextWrapper(context);
            var file = new FileInfo(Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".txt"));

            using (var sw = file.CreateText())
            {
                sw.WriteLine("something");
            }

            a.AddResultFile(file.FullName);
        }
    }
}
