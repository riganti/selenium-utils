using System;
using System.IO;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    internal class TestContextWrapper : ITestContext
    {
        private readonly TestInstance testInstance;
        private ITestOutputHelper context;

        public TestContextWrapper(ITestOutputHelper context, TestInstance testInstance)
        {
            this.context = context;
            this.testInstance = testInstance;
        }

        public string DeploymentDirectory
        {
            get
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory()
                        .Substring(0,
                            Directory.GetCurrentDirectory().IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase)),
                    "TestResults");
                EnsureDirectoryExists(path);
                return path;
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string FullyQualifiedTestClassName { get; set; }
        public string TestName { get; set; }

        public UnitTestResult CurrentTestResult
        {
            get
            {
                return UnitTestResult.Unknown;
                //UnitTestResult value;
                //Enum.TryParse(context.CurrentTestOutcome.ToString(), out value);
                //return value;
            }
        }

    

        public void AddResultFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public IWebBrowser CurrentWebBrowser => testInstance.CurrentWebBrowser;

        public string BaseUrl => testInstance.TestConfiguration.BaseUrl;

        public void WriteLine(string format, params object[] args)
        {
            context.WriteLine(format, args);
        }
    }
}