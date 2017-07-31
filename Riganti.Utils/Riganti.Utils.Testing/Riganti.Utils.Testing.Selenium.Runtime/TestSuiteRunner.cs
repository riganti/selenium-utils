using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Discovery;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime
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
            var factoryResolver = new WebBrowserFactoryResolver();
            return factoryResolver.CreateWebBrowserFactories(Configuration, TestContextAccessor, LoggerService, searchAssemblies);
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


        public void RunInAllBrowsers(Action<BrowserWrapper> action, [CallerMemberName]string callerMemberName = "", [CallerFilePath]string callerFilePath = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            var testName = $"{callerMemberName}";
            this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Entering RunInAllBrowsers from {callerFilePath}:{callerLineNumber}");

            try
            {
                if (Configuration.TestRunOptions.RunInParallel)
                {
                    RunInAllBrowsersParallel(testName, action);
                }
                else
                {
                    RunInAllBrowsersSequential(testName, action);
                }
            }
            finally
            {
                this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Leaving RunInAllBrowsers");
            }
        }

        private void RunInAllBrowsersSequential(string testName, Action<BrowserWrapper> action)
        {
            foreach (var testConfiguration in testConfigurations)
            {
                RunSingleTest(testConfiguration, testName, action).Wait();
            }
        }

        private void RunInAllBrowsersParallel(string testName, Action<BrowserWrapper> action)
        {
            Parallel.ForEach(testConfigurations, c => RunSingleTest(c, testName, action).Wait());
        }

        private async Task RunSingleTest(TestConfiguration testConfiguration, string testName, Action<BrowserWrapper> action)
        {
            var testFullName = $"{testName} for {testConfiguration.BaseUrl} in {testConfiguration.Factory.Name}";

            this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testFullName}: Starting test run");
            try
            {
                var instance = new TestInstance(this, testConfiguration, testFullName, action);
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
