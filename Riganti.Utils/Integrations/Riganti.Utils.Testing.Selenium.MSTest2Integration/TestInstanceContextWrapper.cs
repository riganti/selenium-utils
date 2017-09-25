using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.Core
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