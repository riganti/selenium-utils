﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories
{
    public interface IWebBrowserFactory
    {
        string Name { get; }

        IDictionary<string, string> Options { get; }
        
        TestSuiteRunner TestSuiteRunner { get; }

        Task<IWebBrowser> AcquireBrowser();
        
        Task ReleaseBrowser(IWebBrowser browser);

    }
}