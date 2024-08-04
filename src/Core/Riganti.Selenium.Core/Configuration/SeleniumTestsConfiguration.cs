using System.Collections.Generic;

namespace Riganti.Selenium.Core.Configuration
{
    public class SeleniumTestsConfiguration
    {
        public Dictionary<string, FactoryConfiguration> Factories { get; } = new Dictionary<string, FactoryConfiguration>();

        public List<string> BaseUrls { get; } = new List<string>();

        public TestRunOptions TestRunOptions { get; } = new TestRunOptions();

        public LoggingOptions Logging { get; } = new LoggingOptions();
    }
}