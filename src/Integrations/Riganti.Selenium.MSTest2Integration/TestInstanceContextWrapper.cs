using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    internal class TestInstanceContextWrapper : TestContextWrapper, ITestInstanceContext
    {
        private readonly TestInstance testInstance;

        public TestInstanceContextWrapper(TestContext context, TestInstance testInstance)
        : base(context)
        {
            this.testInstance = testInstance;
        }

        public IWebBrowser CurrentWebBrowser => testInstance.CurrentWebBrowser;

        public string BaseUrl => testInstance.TestConfiguration.BaseUrl;
    }
}