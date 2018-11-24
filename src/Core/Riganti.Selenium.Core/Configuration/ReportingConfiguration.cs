using System.Collections.Generic;

namespace Riganti.Selenium.Core.Configuration
{
    public class ReportingConfiguration
    {
        /// <summary>
        /// Url of endpoint for reporting test results
        /// </summary>
        public string ReportTestResultUrl { get; set; }

        /// <summary>
        /// Determine whether the reporter is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Additional configuration
        /// </summary>
        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();
    }
}