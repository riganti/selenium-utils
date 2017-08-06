using System.Collections.Generic;

namespace Riganti.Utils.Testing.Selenium.Core.Configuration
{
    public class LoggingOptions
    {

        public Dictionary<string, LoggerConfiguration> Loggers { get; } = new Dictionary<string, LoggerConfiguration>();

    }
}