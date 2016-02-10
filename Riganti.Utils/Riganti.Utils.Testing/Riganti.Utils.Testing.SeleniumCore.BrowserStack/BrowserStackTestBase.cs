using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore.BrowserStack
{
    public abstract class BrowserStackTestBase : SeleniumTestBase
    {
        public abstract IEnumerable<IBrowserStackDriverFactory> SetBrowserFactories();

        private void Init()
        {
            if (BrowserStackConfiguration.EnableBrowserStack)
            {
                if (BrowserStackConfiguration.BrowserStackOnly)
                {
                    FactoryRegistry.Clear();
                }
                //add supported browsers
                SetBrowserFactories().ToList().ForEach(f => FactoryRegistry.RegisterBrowserFactory(f));
                if (SeleniumTestsConfiguration.FastMode)
                {
                    SeleniumTestsConfiguration.FastMode = false;
                    Log("FastMode is not supported for BrowserStack. This setting is going to be ignored.");
                }
            }
        }

        protected BrowserStackTestBase()
        {
            Init();
        }
    }
}