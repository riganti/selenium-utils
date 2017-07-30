using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Discovery
{
    public class WebBrowserFactoryResolver
    {

        public Dictionary<string, IWebBrowserFactory> CreateWebBrowserFactories(SeleniumTestsConfiguration configuration, Assembly[] assemblies)
        {
            // find all factories
            var foundTypes = DiscoverFactories(assemblies);
            var factories = InstantiateFactories(configuration, foundTypes);

            // create instances and configure them
            var result = new Dictionary<string, IWebBrowserFactory>();
            foreach (var factoryConfiguration in configuration.Factories.Where(f => f.Value.Enabled))
            {
                // try to find factory instance
                var instance = factories.SingleOrDefault(f => f.Name == factoryConfiguration.Key);
                if (instance == null)
                {
                    throw new SeleniumTestConfigurationException($"The factory '{factoryConfiguration.Key}' was not found!");
                }
                
                // load options
                foreach (var entry in factoryConfiguration.Value.Options)
                {
                    instance.Options[entry.Key] = entry.Value;
                }

                // return the instance
                result[factoryConfiguration.Key] = instance;
            }
            return result;
        }

        private IEnumerable<Type> DiscoverFactories(Assembly[] assemblies)
        {
            var foundTypes = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(t => typeof(IWebBrowserFactory).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract);
            return foundTypes;
        }

        private IList<IWebBrowserFactory> InstantiateFactories(SeleniumTestsConfiguration configuration, IEnumerable<Type> foundTypes)
        {
            var loggerService = CreateLoggerService(configuration);
            var testContextAccessor = CreateTestContextAccessor();

            var instances = new List<IWebBrowserFactory>();
            foreach (var type in foundTypes)
            {
                try
                {
                    var instance = (IWebBrowserFactory) Activator.CreateInstance(type, configuration, loggerService, testContextAccessor);
                    instances.Add(instance);
                }
                catch (Exception ex)
                {
                    throw new SeleniumTestConfigurationException($"Failed to create an instance of the factory '{type}'! Make sure that the factory has a constructor with the following parameters (the order must match): {typeof(SeleniumTestsConfiguration)}, {typeof(LoggerService)}, {typeof(TestContextAccessor)}", ex);
                }
            }
            return instances;
        }

        private TestContextAccessor CreateTestContextAccessor()
        {
            return new TestContextAccessor();
        }

        private LoggerService CreateLoggerService(SeleniumTestsConfiguration configuration)
        {
            return new LoggerService(configuration);
        }
    }
}
