using System;
using System.IO;
using Riganti.Selenium.Core.Abstractions;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core
{
    public class TestContextWrapper : ITestContext
    {
        protected ITestOutputHelper context;

        public TestContextWrapper(ITestOutputHelper context)
        {
            this.context = context;
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

        protected static void EnsureDirectoryExists(string path)
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
                //Enum.TryParse(instanceContext.CurrentTestOutcome.ToString(), out value);
                //return value;
            }
        }



        public virtual void AddResultFile(string fileName)
        {
            // TODO: add files
           
            //var fullpath = Path.GetFullPath(fileName);
            //var fileInfo = new FileInfo(fileName);
            //var fullpath2 = fileInfo.FullName;
            //var uri = new Uri(fileName);
            //var isAbsolut = uri.IsAbsoluteUri;

            // var destination = DeploymentDirectory
        }
        public void WriteLine(string format, params object[] args)
        {
            context.WriteLine(format, args);
        }
    }
}