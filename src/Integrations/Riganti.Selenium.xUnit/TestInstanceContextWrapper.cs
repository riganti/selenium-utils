﻿using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Drivers;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core
{
    internal class TestInstanceContextWrapper : TestContextWrapper, ITestInstanceContext
    {
        private readonly TestInstance testInstance;

        public TestInstanceContextWrapper(ITestOutputHelper context, TestInstance testInstance) :base(context)
        {
            this.testInstance = testInstance;
        }


        public IWebBrowser CurrentWebBrowser => testInstance.CurrentWebBrowser;

        public string BaseUrl => testInstance.TestConfiguration.BaseUrl;

    }
}