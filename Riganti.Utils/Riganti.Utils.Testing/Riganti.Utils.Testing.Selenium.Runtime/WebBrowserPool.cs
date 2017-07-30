using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public class WebBrowserPool
    {

        private readonly object locker = new object();
        private readonly List<IWebBrowser> pool = new List<IWebBrowser>();


        public async Task<IWebBrowser> GetOrCreateBrowser(IWebBrowserFactory factory)
        {
            IWebBrowser instance = null;
            while (true)
            {
                // try to find the browser in the pool
                lock (locker)
                {
                    instance = pool.FirstOrDefault(b => b.Factory == factory);
                }

                // if the browser is not in the cache, acquire a new one
                if (instance == null)
                {
                    instance = await factory.AcquireBrowser();
                }

                if (instance != null)
                {
                    break;
                }

                // TODO: implement queue using TaskCompletionSource
                await Task.Delay(5000);
            }

            return instance;
        }


        public void ReturnBrowserToPool(IWebBrowser webBrowser)
        {
            lock (locker)
            {
                pool.Add(webBrowser);
            }
        }


        public async Task DisposeAllBrowsers()
        {
            foreach (var browser in pool)
            {
                try
                {
                    await browser.Factory.ReleaseBrowser(browser);
                }
                catch
                {
                    // ignore
                }
            }
        }

    }
}
