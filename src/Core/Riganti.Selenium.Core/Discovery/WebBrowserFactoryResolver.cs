﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Discovery
{
    public class WebBrowserFactoryResolver<T> where T : TestSuiteRunner
    {
        /// <summary>
        /// Discovers factories, apply configuration and returns enabled factories  
        /// </summary>
        public Dictionary<string, IWebBrowserFactory> CreateWebBrowserFactories(T runner, IEnumerable<Assembly> assemblies)
        {
            // find all factories
            var foundTypes = DiscoverFactories(assemblies);
            var factories = InstantiateFactories(runner, foundTypes);

            // create instances and configure them
            var result = new Dictionary<string, IWebBrowserFactory>();
            foreach (var factoryConfiguration in runner.Configuration.Factories.Where(f => f.Value.Enabled))
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

                //load capabilities
                foreach (var entry in factoryConfiguration.Value.Capabilities)
                {
                    instance.Capabilities.Add(entry);
                }

                // return the instance
                result[factoryConfiguration.Key] = instance;
            }
            return result;
        }
        /// <summary>
        /// Iterates all types of assemblies and returns IWebBrowserFactory types that can be initiated.
        /// </summary>
        protected virtual IEnumerable<Type> DiscoverFactories(IEnumerable<Assembly> assemblies)
        {
            var foundTypes = GetExportedTypes(assemblies)
                .Where(t => typeof(IWebBrowserFactory).IsAssignableFrom(t))
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

        private IList<IWebBrowserFactory> InstantiateFactories(T testSuiteRunner, IEnumerable<Type> foundTypes)
        {
            var instances = new List<IWebBrowserFactory>();
            foreach (var type in foundTypes)
            {
                try
                {
                    var instance = (IWebBrowserFactory)Activator.CreateInstance(type, testSuiteRunner);
                    instances.Add(instance);
                }
                catch (Exception ex)
                {
                    throw new SeleniumTestConfigurationException($"Failed to create an instance of the factory '{type}'! Make sure that the factory has a constructor with the exactly one parameter of type {typeof(T)}.", ex);
                }
            }
            return instances;
        }

    }
}
