﻿using System.Collections.Generic;

namespace Riganti.Utils.Testing.Selenium.Runtime.Configuration
{
    public class LoggingOptions
    {

        public int LoggingPriorityMaximum { get; set; } = 8;

        public Dictionary<string, LoggerConfiguration> Loggers { get; } = new Dictionary<string, LoggerConfiguration>();

    }
}