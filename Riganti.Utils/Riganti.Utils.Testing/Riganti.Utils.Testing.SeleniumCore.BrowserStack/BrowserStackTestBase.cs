using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore.BrowserStack
{
    public abstract class BrowserStackTestBase : SeleniumTestBase
    {
        public abstract IEnumerable<IBrowserStackDriverFactory> SetBrowserFactories();
        protected override List<IWebDriverFactory> BrowserFactories => FactoryRegistry.BrowserFactories;

        private void Init()
        {
            SetBrowserFactories().ToList().ForEach(f => FactoryRegistry.RegisterBrowserFactory(f));
            if (SeleniumTestsConfiguration.FastMode)
            {
                SeleniumTestsConfiguration.FastMode = false;
                Log("FastMode is not supported for BrowserStack. This setting is going to be ignored.");
            }
        }
        protected BrowserStackTestBase()
        {
            SeleniumTestsConfiguration.PlainMode = true;
            Init();
        }
    }
}