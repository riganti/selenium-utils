using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core.Configuration
{
    public class ConfigurationBuilder
    {
        private readonly SeleniumTestsConfiguration configuration;

        public ConfigurationBuilder()
        {
            configuration = new SeleniumTestsConfiguration();
        }

        public ConfigurationBuilder AddJsonFile(string file, bool isOptional = true)
        {
            if (File.Exists(file))
            {
                var json = File.ReadAllText(file, Encoding.UTF8);
                JsonConvert.PopulateObject(json, configuration);
            }
            else if (!isOptional)
            {
                throw new SeleniumTestConfigurationException($"The configuration file '{file}' was not found!");
            }

            return this;
        }

        public SeleniumTestsConfiguration Build()
        {
            return configuration;
        }

    }
}
