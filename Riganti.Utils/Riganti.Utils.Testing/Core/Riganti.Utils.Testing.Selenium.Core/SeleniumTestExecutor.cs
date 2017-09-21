using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// This class represents the funcation of UI tests based on selenium. Provides logging, re-try logic, screenshots, etc..
    /// </summary>
    public abstract class SeleniumTestExecutor : ISeleniumTest
    {

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

            var defaultConfigurationPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "seleniumconfig.json");
            builder.AddJsonFile(defaultConfigurationPath, true);

            return builder;
        }

        protected abstract TestSuiteRunner InitializeTestSuiteRunner(SeleniumTestsConfiguration configuration);

        public Guid CurrentScope { get; set; }


        ///// <summary>
        ///// Runs the specified testBody in all configured browsers.
        ///// </summary>
        //protected virtual void RunInAllBrowsers(Action<BrowserWrapper> testBody, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        //{
        //    TestSuiteRunner.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        //}



    }
}