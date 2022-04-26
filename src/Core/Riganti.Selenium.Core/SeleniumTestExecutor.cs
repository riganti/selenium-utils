using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Configuration;

namespace Riganti.Selenium.Core
{
    /// <summary>
    /// This class represents the funcation of UI tests based on selenium. Provides logging, re-try logic, screenshots, etc..
    /// </summary>
    public abstract class SeleniumTestExecutor : ISeleniumTest
    {
        private static object testSuiteRunnerLocker = new object();
        private static TestSuiteRunner testSuiteRunner = null;

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
                            Process.GetCurrentProcess().Exited += OnExited;
                        }
                    }
                }
                return testSuiteRunner;
            }
        }

        private void OnExited(object sender, EventArgs e)
        {
            testSuiteRunner.Dispose();
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
            // default
            var url = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var configurationPath = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(url.AbsolutePath)), "seleniumconfig.json");
            Trace.WriteLine($"Default selenium configuration location: {configurationPath}");
            if (File.Exists(configurationPath))
            {
                return configurationPath;
            }
            Trace.WriteLine($"File '{configurationPath}' does not exist.");

            // alternative #1
            configurationPath = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("seleniumconfig.json", SearchOption.AllDirectories).First().FullName;
            if (File.Exists(configurationPath))
            {
                return configurationPath;
            }
            Trace.WriteLine($"File '{configurationPath}' does not exist.");

            throw new SeleniumTestConfigurationException("Cannot find seleniumconfig.json file.");
        }

        protected abstract TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration);

        public Guid CurrentScope { get; set; }
    }
}