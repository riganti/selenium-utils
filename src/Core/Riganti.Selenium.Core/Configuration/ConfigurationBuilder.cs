using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.Core.Configuration
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
                Trace.WriteLine($"Loading selenium configuration file on '{file}'.");
                var json = File.ReadAllText(file, Encoding.UTF8);
                JsonConvert.PopulateObject(json, configuration);
            }
            else if (!isOptional)
            {
                throw new SeleniumTestConfigurationException($"The configuration file '{file}' was not found!");
            }
            else
            {
                Trace.WriteLine($"Selenium configuration file on '{file}'.");
            }

            return this;
        }

        public ConfigurationBuilder Configure(Action<SeleniumTestsConfiguration> configureAction)
        {
            configureAction(configuration);
            return this;
        }

        public SeleniumTestsConfiguration Build()
        {
            return configuration;
        }

    }
}
