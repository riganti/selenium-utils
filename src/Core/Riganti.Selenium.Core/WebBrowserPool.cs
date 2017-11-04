using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Drivers;
using Riganti.Selenium.Core.Factories;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public class WebBrowserPool
    {
        private readonly TestSuiteRunner runner;

        private readonly object locker = new object();
        private readonly List<IWebBrowser> pool = new List<IWebBrowser>();

        public WebBrowserPool(TestSuiteRunner runner)
        {
            this.runner = runner;
        }


        public async Task<IWebBrowser> GetOrCreateBrowser(IWebBrowserFactory factory)
        {
            IWebBrowser instance = null;
            while (true)
            {
                // try to find the browser in the pool
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) Looking for the browser in the browser {factory.Name} pool cache.");
                lock (locker)
                {
                    instance = pool.FirstOrDefault(b => b.Factory == factory);

                    if (instance != null)
                    {
                        pool.Remove(instance);
                    }
                }

                // if the browser is not in the cache, acquire a new one
                if (instance == null)
                {
                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) No browser {factory.Name} found in the cache, requesting a new one.");
                    instance = await factory.AcquireBrowser();
                }

                if (instance != null)
                {
                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) Browser instance {instance.UniqueName} acquired successfully.");
                    break;
                }

                // TODO: implement queue using TaskCompletionSource
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) No instances of {factory.Name} browser available, retrying in 5 seconds...");
                await Task.Delay(5000);
            }

            return instance;
        }

     

        public async Task ReturnBrowserToPool(IWebBrowser webBrowser)
        {
            if (webBrowser.Factory is IReusableWebBrowserFactory reusableFactory)
            {
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) Clearing browser {webBrowser.UniqueName} state.");
                try
                {
                    reusableFactory.ClearBrowserState(webBrowser);

                    lock (locker)
                    {
                        pool.Add(webBrowser);
                    }
                    runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) Browser {webBrowser.UniqueName} returned to the pool cache.");
                    return;
                }
                catch (Exception ex)
                {
                    runner.LogError(new Exception($"(#{Thread.CurrentThread.ManagedThreadId}) Failed to clear browser {webBrowser.UniqueName} state", ex));
                }
            }

            await DisposeBrowser(webBrowser);
        }

        public async Task DisposeBrowser(IWebBrowser browser)
        {
            try
            {
                runner.LogVerbose($"(#{Thread.CurrentThread.ManagedThreadId}) Disposing browser {browser.UniqueName}.");
                await browser.Factory.ReleaseBrowser(browser);
            }
            catch (Exception ex)
            {
                runner.LogError(new Exception($"Cannot release browser {browser.UniqueName}.", ex));
            }
        }

        public async Task DisposeAllBrowsers()
        {
            foreach (var browser in pool)
            {
                try
                {
                    runner.LogVerbose($"Disposing browser {browser.UniqueName}.");
                    await browser.Factory.ReleaseBrowser(browser);
                }
                catch (Exception ex)
                {
                    runner.LogError(new Exception($"Cannot release browser {browser.UniqueName}.", ex));
                }
            }
        }
    }
}
