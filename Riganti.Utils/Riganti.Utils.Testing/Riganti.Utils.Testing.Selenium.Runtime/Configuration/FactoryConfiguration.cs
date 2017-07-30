using System.Collections.Generic;

namespace Riganti.Utils.Testing.Selenium.Runtime.Configuration
{
    public class FactoryConfiguration
    {

        public bool Enabled { get; set; } = true;

        public Dictionary<string, string> Options { get; } = new Dictionary<string, string>();

    }
}