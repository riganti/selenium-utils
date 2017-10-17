using System.Collections.Generic;

namespace Riganti.Utils.Testing.Selenium.Core.Configuration
{
    public class FactoryConfiguration
    {

        public bool Enabled { get; set; } = true;

        public Dictionary<string, string> Options { get; } = new Dictionary<string, string>();

        public IList<string> Capabilities { get; } = new List<string>();
    }
}