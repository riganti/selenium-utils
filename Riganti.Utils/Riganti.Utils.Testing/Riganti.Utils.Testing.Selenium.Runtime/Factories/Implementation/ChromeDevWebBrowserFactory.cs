using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Configuration;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation;
using Riganti.Utils.Testing.Selenium.Runtime.Logging;

namespace Riganti.Utils.Testing.Selenium.Runtime.Factories.Implementation
{
    public class ChromeDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "chrome:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new ChromeDevWebBrowser(this);
        }

        public ChromeDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
