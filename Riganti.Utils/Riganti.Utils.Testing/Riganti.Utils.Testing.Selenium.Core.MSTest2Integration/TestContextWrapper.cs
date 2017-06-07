using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

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

        public string DeploymentDirectory => context.DeploymentDirectory;

        public string FullyQualifiedTestClassName => context.FullyQualifiedTestClassName;

        public IDictionary Properties => context.Properties;

        public string ResultsDirectory => context.ResultsDirectory;

        public string TestDeploymentDir => context.TestDeploymentDir;

        public string TestDir => context.TestDir;

        public string TestLogsDir => context.TestLogsDir;

        public string TestName => context.TestName;

        public string TestResultsDirectory => context.TestResultsDirectory;

        public string TestRunDirectory => context.TestRunDirectory;

        public string TestRunResultsDirectory => context.TestRunResultsDirectory;

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