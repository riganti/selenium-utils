using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Selenium.Core.Configuration
{
    public class SeleniumTestsConfiguration
    {
        public Dictionary<string, FactoryConfiguration> Factories { get; } = new Dictionary<string, FactoryConfiguration>();

        public List<string> BaseUrls { get; } = new List<string>();

        public TestRunOptions TestRunOptions { get; } = new TestRunOptions();

        public LoggingOptions Logging { get; } = new LoggingOptions();
        public ReportingOptions Reporting { get; } = new ReportingOptions();
    }
}