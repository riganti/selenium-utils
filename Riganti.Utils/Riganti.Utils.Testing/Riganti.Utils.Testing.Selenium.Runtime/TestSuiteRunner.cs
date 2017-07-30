using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Discovery;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public class TestSuiteRunner
    {
        private readonly SeleniumTestsConfiguration configuration;

        private readonly Dictionary<string, IWebBrowserFactory> factories;

        private readonly List<TestConfiguration> testConfigurations;

        public WebBrowserPool WebBrowserPool { get; }

        public ITestContextProvider TestContextProvider { get; }


        public TestSuiteRunner(SeleniumTestsConfiguration configuration, ITestContextProvider testContextProvider)
        {
            this.configuration = configuration;
            this.TestContextProvider = testContextProvider;
            this.WebBrowserPool = new WebBrowserPool();

            // load configuration and get all factories
            factories = CreateWebBrowserFactories();

            // get test configurations
            testConfigurations = GetTestConfigurations();
        }




        private Dictionary<string, IWebBrowserFactory> CreateWebBrowserFactories()
        {
            var factoryResolver = new WebBrowserFactoryResolver();
            return factoryResolver.CreateWebBrowserFactories(configuration, new[] { Assembly.GetExecutingAssembly() });
        }

        private List<TestConfiguration> GetTestConfigurations()
        {
            return factories.SelectMany(f => configuration.BaseUrls.Select(u => new TestConfiguration()
            {
                Factory = f.Value,
                BaseUrl = u
            }))
            .ToList();
        }


        public void RunInAllBrowsers(Action<BrowserWrapper> action)
        {
            if (configuration.TestRunOptions.RunInParallel)
            {
                RunInAllBrowsersParallel(action);
            }
            else
            {
                RunInAllBrowsersSequential(action);
            }
        }

        private void RunInAllBrowsersSequential(Action<BrowserWrapper> action)
        {
            foreach (var testConfiguration in testConfigurations)
            {
                RunSingleTest(testConfiguration, action).Wait();
            }
        }

        private void RunInAllBrowsersParallel(Action<BrowserWrapper> action)
        {
            Parallel.ForEach(testConfigurations, c => RunSingleTest(c, action).Wait());
        }

        private async Task RunSingleTest(TestConfiguration testConfiguration, Action<BrowserWrapper> action)
        {
            var instance = new TestInstance(configuration, this, testConfiguration, action);
            await instance.RunAsync();
        }


        public void GlobalCleanup()
        {
            WebBrowserPool.DisposeAllBrowsers().Wait();
        }
    }
}
