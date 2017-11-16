using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Api;

namespace Riganti.Selenium.Core
{
    /// <summary>
    /// This class represents the funcation of UI tests based on selenium. Provides logging, re-try logic, screenshots, etc..
    /// </summary>
    public abstract class SeleniumTestExecutor : ISeleniumTest {

        private static object testSuiteRunnerLocker = new object();
        private static TestSuiteRunner testSuiteRunner = null;
        //public virtual ISeleniumAssertion Assert { get; set; } = new SeleniumAssertion();

        public TestSuiteRunner TestSuiteRunner
        {
            get
            {
                if (testSuiteRunner == null)
                {
                    lock (testSuiteRunnerLocker)
                    {
                        if (testSuiteRunner == null)
                        {
                            var builder = CreateConfigurationBuilder();
                            var configuration = builder.Build();

                            if (!configuration.Factories.Any())
                            {
                                throw new SeleniumTestConfigurationException("The configuration is not correct! No web browser factories were registered!");
                            }

                            testSuiteRunner = InitializeTestSuiteRunner(configuration);

                            AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
                            {
                                testSuiteRunner.Dispose();
                            };
                        }
                    }
                }
                return testSuiteRunner;
            }
        }
        /// <summary>
        /// Creates configuration builder from seleniumconfig.json
        /// </summary>
        /// <returns></returns>
        protected virtual ConfigurationBuilder CreateConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder();

            string defaultConfigurationPath = ResolveConfigurationFilePath();
            builder.AddJsonFile(defaultConfigurationPath, true);

            return builder;
        }
        /// <summary>
        /// Resolves path to seleniumconfig.json
        /// </summary>
        public virtual string ResolveConfigurationFilePath()
        {
            var url = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var defaultConfigurationPath = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(url.AbsolutePath)), "seleniumconfig.json");
            Trace.WriteLine($"Default selenium configuration location: {defaultConfigurationPath}");
            return defaultConfigurationPath;
        }

        protected abstract TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration);

        public Guid CurrentScope { get; set; }

    }
}