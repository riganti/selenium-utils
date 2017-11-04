using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Logging;

namespace Riganti.Selenium.Core.Discovery
{
    public class LoggerResolver
    {

        public List<ILogger> CreateLoggers(SeleniumTestsConfiguration configuration, Assembly[] assemblies)
        {
            // find all loggers
            var foundTypes = DiscoverLoggers(assemblies);
            var loggers = InstantiateLoggers(foundTypes);

            // create instances and configure them
            var result = new List<ILogger>();
            foreach (var loggerConfiguration in configuration.Logging.Loggers.Where(f => f.Value.Enabled))
            {
                // try to find logger instance
                var instance = loggers.SingleOrDefault(f => f.Name == loggerConfiguration.Key);
                if (instance == null)
                {
                    throw new SeleniumTestConfigurationException($"The logger '{loggerConfiguration.Key}' was not found!");
                }

                // load options
                foreach (var entry in loggerConfiguration.Value.Options)
                {
                    instance.Options[entry.Key] = entry.Value;
                }

                // return the instance
                result.Add(instance);
            }
            return result;
        }

        private IEnumerable<Type> DiscoverLoggers(Assembly[] assemblies)
        {
            var foundTypes = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(t => typeof(ILogger).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract);
            return foundTypes;
        }

        private IList<ILogger> InstantiateLoggers(IEnumerable<Type> foundTypes)
        {
            var instances = new List<ILogger>();
            foreach (var type in foundTypes)
            {
                try
                {
                    var instance = (ILogger)Activator.CreateInstance(type);
                    instances.Add(instance);
                }
                catch (Exception ex)
                {
                    throw new SeleniumTestConfigurationException($"Failed to create an instance of the logger '{type}'!", ex);
                }
            }
            return instances;
        }

    }
}
