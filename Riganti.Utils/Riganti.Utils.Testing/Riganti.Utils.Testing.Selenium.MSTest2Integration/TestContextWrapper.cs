using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.Core
{
    internal class TestContextWrapper : ITestContext
    {
        private readonly TestContext context;
        private readonly TestInstance testInstance;

        public TestContextWrapper(TestContext context, TestInstance testInstance)
        {
            this.context = context;
            this.testInstance = testInstance;
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

        public string DeploymentDirectory => context.TestDeploymentDir;
        
        public string TestName => context.TestName;

        public void AddResultFile(string fileName)
        {
            context.AddResultFile(fileName);
        }

        public IWebBrowser CurrentWebBrowser => testInstance.CurrentWebBrowser;

        public string BaseUrl => testInstance.TestConfiguration.BaseUrl;

        public void WriteLine(string format, params object[] args)
        {
            context.WriteLine(format, args);
        }
    }
}