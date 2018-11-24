using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Abstractions.Attributes;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Abstractions.Reporting;
using Riganti.Selenium.Core.Configuration;
using Riganti.Selenium.Core.Discovery;
using Riganti.Selenium.Core.Factories;
using Riganti.Selenium.Core.Logging;
using Riganti.Selenium.Core.Reporting;

namespace Riganti.Selenium.Core
{
    public class TestSuiteRunner : IDisposable
    {
        private Dictionary<string, IWebBrowserFactory> factories;

        private List<TestConfiguration> testConfigurations;
        public List<Assembly> SearchAssemblies { get; private set; }

        public SeleniumTestsConfiguration Configuration { get; }

        public TestContextAccessor TestContextAccessor { get; }

        public WebBrowserPool WebBrowserPool { get; }

        public ITestContextProvider TestContextProvider { get; }

        public IReportingMetadataProvider ReportingMetadataProvider { get; }

        public LoggerService LoggerService { get; set; }
        public ServiceFactory ServiceFactory { get; } = new ServiceFactory();

        public TestSuiteRunner(SeleniumTestsConfiguration configuration, ITestContextProvider testContextProvider, Action<ServiceFactory, TestSuiteRunner> registerServices = null)
        {
            SearchAssemblies = new List<Assembly>() { Assembly.GetExecutingAssembly() };
            ServiceFactory.RegisterTransient<WebBrowserFactoryResolver<TestSuiteRunner>, WebBrowserFactoryResolver<TestSuiteRunner>>();
            ServiceFactory.RegisterTransient<ResultReportersFactory, ResultReportersFactory>();

            registerServices?.Invoke(ServiceFactory, this);

            this.Configuration = configuration;
            this.TestContextProvider = testContextProvider;
            this.WebBrowserPool = new WebBrowserPool(this);
            this.TestContextAccessor = new TestContextAccessor();
            ReportingMetadataProvider = new DefaultReportingMetadataProvider(testContextProvider);
        }

        /// <summary>
        /// Load configuration and get all loggers and factories
        /// </summary>

        private void Initialize()
        {
            if (LoggerService != null)
                return;
            LoggerService = CreateLoggerService(SearchAssemblies);
            factories = CreateWebBrowserFactories();
            var reporters = CreateReporters();
            Reporter = new AggregatedReporter(reporters, ReportingMetadataProvider);

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

        internal AggregatedReporter Reporter { get; set; }

        protected virtual Dictionary<string, ITestResultReporter> CreateReporters()
        {
            var factoryResolver = ServiceFactory.Resolve<ResultReportersFactory>();
            return factoryResolver.CreateReporters(SearchAssemblies, Configuration);
        }

        private LoggerService CreateLoggerService(IEnumerable<Assembly> assemblies)
        {
            var discoveryService = new LoggerResolver();
            var loggers = discoveryService.CreateLoggers(Configuration, assemblies);
            return new LoggerService(loggers);
        }

        protected virtual Dictionary<string, IWebBrowserFactory> CreateWebBrowserFactories()
        {
            var factoryResolver = ServiceFactory.Resolve<WebBrowserFactoryResolver<TestSuiteRunner>>();
            return factoryResolver.CreateWebBrowserFactories(this, SearchAssemblies);
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

        /// <summary>
        /// Runs the test specified by action in all browsers specified in seleniumconfig.json
        /// </summary>
        /// <param name="action">Test definition</param>
        /// <param name="callerMemberName">Name of the method that calls this one. Can be provided by attributes.</param>
        /// <param name="callerFilePath">File name of the method that calls this one. Can be provided by attributes.</param>
        /// <param name="callerLineNumber">Line number of the method that calls this one. Can be provided by attributes.</param>
        public virtual void RunInAllBrowsers(ISeleniumTest testClass, Action<IBrowserWrapper> action, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            Initialize();
            var testName = callerMemberName;
            this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Entering RunInAllBrowsers from {callerFilePath}:{callerLineNumber}");

            try
            {
                FormatFinalException(() =>
                {
                    if (Configuration.TestRunOptions.RunInParallel)
                    {
                        RunInAllBrowsersParallel(testClass, testName, action);
                    }
                    else
                    {
                        RunInAllBrowsersSequential(testClass, testName, action);
                    }
                });
                Reporter.ReportSuccessfulTest(testName, callerFilePath, callerLineNumber);
            }
            catch (Exception ex)
            {
                Reporter.ReportFailedTest(ex, testName, callerFilePath, callerLineNumber);
                throw;
            }
            finally
            {
                this.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Leaving RunInAllBrowsers");
            }
        }

        private void RunInAllBrowsersSequential(ISeleniumTest testClass, string testName, Action<IBrowserWrapper> action)
        {
            var skipBrowserAttributes = SkipBrowserAttribute.TryToRetrieveFromStackTrace();

            foreach (var testConfiguration in testConfigurations)
            {
                var skipBrowserAttribute = skipBrowserAttributes.FirstOrDefault(a => a.BrowserName == testConfiguration.Factory.Name);
                if (skipBrowserAttribute != null)
                {
                    this.LogInfo($"(#{Thread.CurrentThread.ManagedThreadId}) {testName}: Test was skipped for " +
                        $"browser: {skipBrowserAttribute.BrowserName}, Reason: {skipBrowserAttribute.Reason}");

                    continue;
                }

                RunSingleTest(testClass, testConfiguration, testName, action).Wait();
            }
        }

        private static void FormatFinalException(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            try
            {
                action();
            }
            catch (Exception e)
            {
                var exceptions = new List<Exception>();
                if (e is AggregateException a1)
                {
                    exceptions.AddRange(a1.InnerExceptions);
                }
                else if (e is SeleniumTestFailedException a)
                {
                    exceptions.AddRange(a.InnerExceptions);
                }
                throw new SeleniumTestFailedException(exceptions);
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