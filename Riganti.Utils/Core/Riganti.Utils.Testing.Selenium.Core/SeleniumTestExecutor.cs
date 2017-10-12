using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
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

        protected virtual ConfigurationBuilder CreateConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder();

            var url = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var defaultConfigurationPath = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(url.AbsolutePath)), "seleniumconfig.json");
            builder.AddJsonFile(defaultConfigurationPath, true);

            return builder;
        }

        protected abstract TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration);

        public Guid CurrentScope { get; set; }

    }
}