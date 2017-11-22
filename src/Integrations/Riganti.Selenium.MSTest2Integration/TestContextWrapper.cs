using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    internal class TestContextWrapper : ITestContext
    {
        public UnitTestResult CurrentTestResult
        {
            get
            {
                UnitTestResult value;
                Enum.TryParse(context.CurrentTestOutcome.ToString(), out value);
                return value;
            }
        }


        protected readonly TestContext context;
        public TestContextWrapper(TestContext context)
        {
            this.context = context;
        }
        public string TestName => context.TestName;

        public void AddResultFile(string fileName)
        {
            //TODO: netappcore - add support for files 

#if NET461
            context.AddResultFile(fileName);
#endif
        }
        public string DeploymentDirectory
        {
            get
            {
#if NET461
                return context.TestDeploymentDir;
#endif
                return null;
            }
        }

        public void WriteLine(string format, params object[] args)
        {
            context.WriteLine(format, args);
        }
        public string FullyQualifiedTestClassName => context.FullyQualifiedTestClassName;


    }
}