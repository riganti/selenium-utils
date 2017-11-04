using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core
{
    internal class TestInstanceContextWrapper : TestContextWrapper, ITestInstanceContext
    {
        public string BaseUrl => testInstance.TestConfiguration.BaseUrl;
        public IWebBrowser CurrentWebBrowser => testInstance.CurrentWebBrowser;
        private readonly TestInstance testInstance;

        public TestInstanceContextWrapper(TestContext context, TestInstance testInstance) : base(context)
        {
            this.testInstance = testInstance;
        }


    }

    internal class TestContextWrapper : ITestContext
    {
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

        public string DeploymentDirectory => context.TestDeploymentDir;

        public string TestName => context.TestName;

        private readonly TestContext context;
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