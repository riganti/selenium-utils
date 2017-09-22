using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;
using Riganti.Utils.Testing.Selenium.Core.Discovery;
using Riganti.Utils.Testing.Selenium.Core.Factories;
using Riganti.Utils.Testing.Selenium.Core.Logging;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class TestSuiteRunner : IDisposable 
    {

        private readonly Dictionary<string, IWebBrowserFactory> factories;

        private readonly List<TestConfiguration> testConfigurations;
        private readonly Assembly[] searchAssemblies;


        public SeleniumTestsConfiguration Configuration { get; }
        
        public TestContextAccessor TestContextAccessor { get; }

        public WebBrowserPool WebBrowserPool { get; }

        public ITestContextProvider TestContextProvider { get; }

        public LoggerService LoggerService { get; }
        public ServiceFactory ServiceFactory { get; } = new ServiceFactory();



        public TestSuiteRunner(SeleniumTestsConfiguration configuration, ITestContextProvider testContextProvider)
        {
            searchAssemblies = new[] { Assembly.GetExecutingAssembly() };
            
            this.Configuration = configuration;
            this.TestContextProvider = testContextProvider;
            this.WebBrowserPool = new WebBrowserPool(this);
            this.TestContextAccessor = new TestContextAccessor();

            // load configuration and get all loggers and factories
            LoggerService = CreateLoggerService(searchAssemblies);
            factories = CreateWebBrowserFactories();

            this.LogInfo("RIGANTI Selenium-Utils Test framework initialized.");
            this.LogVerbose("WebBrowserFactories discovered: ");
            foreach (var factory in factories)
            {
                this.LogVerbose("* " + factory.Key);
            }
            this.LogVerbose("Loggers discovered: ");
            foreach (var logger in LoggerService.Loggers)
            {
                this.LogVerbose("* " + logger.Name);
            }

            // get test configurations
            testConfigurations = GetTestConfigurations();

            this.LogVerbose("Test configurations: ");
            foreach (var testConfiguration in testConfigurations)
            {
                this.LogVerbose($"* [{testConfiguration.BaseUrl}] {testConfiguration.Factory.Name}");
            }
        }


        private LoggerService CreateLoggerService(Assembly[] assemblies)
        {
            var discoveryService = new LoggerResolver();
            var loggers = discoveryService.CreateLoggers(Configuration, assemblies);
            return new LoggerService(loggers);
        }

        private Dictionary<string, IWebBrowserFactory> CreateWebBrowserFactories()
        {
            var factoryResolver = new WebBrowserFactoryResolver<TestSuiteRunner>();
            return factoryResolver.CreateWebBrowserFactories(this, searchAssemblies);
        }

        private List<TestConfiguration> GetTestConfigurations()
        {
            return factories.SelectMany(f => Configuration.BaseUrls.Select(u => new TestConfiguration()
            {
                Factory = f.Value,
                BaseUrl = u
            }))
            .ToList();
        }
        

        public virtual void RunInAllBrowsers(ISeleniumTest testClass, Action<IBrowserWrapper> action, string callerMemberName, string callerFilePath, int callerLineNumber) 
        {
            var testName = callerMemberName;
            this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Entering RunInAllBrowsers from {callerFilePath}:{callerLineNumber}");

            try
            {
                if (Configuration.TestRunOptions.RunInParallel)
                {
                    RunInAllBrowsersParallel(testClass, testName, action);
                }
                else
                {
                    RunInAllBrowsersSequential(testClass, testName, action);
                }
            }
            finally
            {
                this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Leaving RunInAllBrowsers");
            }
        }

        private void RunInAllBrowsersSequential(ISeleniumTest testClass, string testName, Action<IBrowserWrapper> action) 
        {
            foreach (var testConfiguration in testConfigurations)
            {
                RunSingleTest(testClass, testConfiguration, testName, action).Wait();
            }
        }

        private void RunInAllBrowsersParallel(ISeleniumTest testClass, string testName, Action<IBrowserWrapper> action) 
        {
            Parallel.ForEach(testConfigurations, c => RunSingleTest(testClass, c, testName, action).Wait());
        }

        private async Task RunSingleTest(ISeleniumTest testClass, TestConfiguration testConfiguration, string testName, Action<IBrowserWrapper> action) 
        {
            var testFullName = $"{testName} for {testConfiguration.BaseUrl} in {testConfiguration.Factory.Name}";

            this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testFullName}: Starting test run");
            try
            {
                var instance = new TestInstance(this, testClass, testConfiguration, testFullName, action);
                await instance.RunAsync();
            }
            finally
            {
                this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testFullName}: Finishing test run");
            }
        }


        public void Dispose()
        {
            WebBrowserPool.DisposeAllBrowsers().Wait();
        }
    }
}
