using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.Selenium.Core
{
    internal class TestContextWrapper : ITestContext
    {
        private readonly TestContext context;

        public TestContextWrapper(TestContext context)
        {
            this.context = context;
        }

        public UnitTestResult CurrentTestResult
        {
            get
            {
                UnitTestResult value;
                Enum.TryParse(context.CurrentTestOutcome.ToString(), out value);
                return value;
            }
        }

        public string FullyQualifiedTestClassName => context.FullyQualifiedTestClassName;

        public string TestDeploymentDir => context.TestDeploymentDir;

        public string TestLogsDir => context.TestLogsDir;

        public string TestName => context.TestName;

        public void AddResultFile(string fileName)
        {
            context.AddResultFile(fileName);
        }

        public void WriteLine(string format, params object[] args)
        {
            context.WriteLine(format, args);
        }
    }
}