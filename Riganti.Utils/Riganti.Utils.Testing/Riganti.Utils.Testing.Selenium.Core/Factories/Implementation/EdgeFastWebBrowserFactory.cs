using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
{
    public class EdgeFastWebBrowserFactory : FastLocalWebBrowserFactory
    {
        public override string Name => "edge:fast";

        protected override IWebBrowser CreateBrowser()
        {
            return new EdgeFastWebBrowser(this);
        }

        public EdgeFastWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
