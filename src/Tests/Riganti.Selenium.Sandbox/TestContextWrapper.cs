using System;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.Sandbox
{
    internal class TestContextWrapper : ITestContext, ITestInstanceContext
    {
        private readonly TestInstance test;

        public TestContextWrapper(TestInstance test)
        {
            this.test = test;
        }

        public UnitTestResult CurrentTestResult { get; } = UnitTestResult.Inconclusive;


        public string TestName { get; } = "Sandbox";

        public void AddResultFile(string fileName)
        {
            // nop
        }
        
        public string DeploymentDirectory { get; } = null;

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }
        
        public string FullyQualifiedTestClassName { get; } = "Riganti.Selenium.Sandbox";

        public IWebBrowser CurrentWebBrowser => test?.CurrentWebBrowser;

        public string BaseUrl => test?.TestConfiguration.BaseUrl;
    }
}
