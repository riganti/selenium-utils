﻿using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core
{
    public class TestConfiguration
    {

        public IWebBrowserFactory Factory { get; set; }

        public string BaseUrl { get; set; }

    }
}