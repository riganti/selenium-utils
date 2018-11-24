using System;
using System.Collections.Generic;
using System.Text;

namespace Riganti.Selenium.Core.Configuration
{
    public class ReportingOptions
    {
        public Dictionary<string, ReportingConfiguration> Reporters { get; } = new Dictionary<string, ReportingConfiguration>();
    }
}