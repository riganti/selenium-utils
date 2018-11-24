using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Abstractions.Reporting;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Discovery
{
    public class ResultReportersFactory
    {
        public Dictionary<string, ITestResultReporter> CreateReporters(IEnumerable<Assembly> assemblies, SeleniumTestsConfiguration configuration)
        {
            // find all factories
            var foundTypes = DiscoverReporters(assemblies);
            var reporters = InstantiateReporters(foundTypes);

            // create instances and configure them
            var result = new Dictionary<string, ITestResultReporter>();
            foreach (var factoryConfiguration in configuration.Reporting.Reporters.Where(s => s.Value.Enabled))
            {
                // try to find factory instance
                var instance = reporters.SingleOrDefault(f => f.Name == factoryConfiguration.Key);
                if (instance == null)
                {
                    throw new SeleniumTestConfigurationException($"The reporter '{factoryConfiguration.Key}' was not found!");
                }

                instance.ReportTestResultUrl = factoryConfiguration.Value.ReportTestResultUrl;

                //load options
                foreach (var entry in factoryConfiguration.Value.Options)
                {
                    instance.Options[entry.Key] = entry.Value;
                }

                // return the instance
                result[factoryConfiguration.Key] = instance;
            }
            return result;
        }

        /// <summary>
        /// Returns discovered types of Reporters that implements <see cref="ITestResultReporter"/>.
        /// </summary>
        /// <param name="assemblies"></param>
        protected virtual IEnumerable<Type> DiscoverReporters(IEnumerable<Assembly> assemblies)
        {
            var foundTypes = GetExportedTypes(assemblies)
                .Where(t => typeof(ITestResultReporter).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract);
            return foundTypes;
        }

        private static IEnumerable<Type> GetExportedTypes(IEnumerable<Assembly> assemblies)
        {
            try
            {
                return assemblies.SelectMany(a => a.GetExportedTypes());
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.ToArray();
            }
        }

        private IList<ITestResultReporter> InstantiateReporters(IEnumerable<Type> foundTypes)
        {
            var instances = new List<ITestResultReporter>();
            foreach (var type in foundTypes)
            {
                try
                {
                    var instance = (ITestResultReporter)Activator.CreateInstance(type);
                    instances.Add(instance);
                }
                catch (Exception ex)
                {
                    throw new SeleniumTestConfigurationException($"Failed to create an instance of the '{type}'! Make sure that the reporter has a parameterless constructor.", ex);
                }
            }
            return instances;
        }
    }
}