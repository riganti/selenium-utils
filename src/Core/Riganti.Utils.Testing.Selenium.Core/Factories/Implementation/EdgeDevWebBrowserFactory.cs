using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Drivers.Implementation;

namespace Riganti.Utils.Testing.Selenium.Core.Factories.Implementation
{
    public class EdgeDevWebBrowserFactory : LocalWebBrowserFactory
    {
        public override string Name => "edge:dev";

        protected override IWebBrowser CreateBrowser()
        {
            return new EdgeDevWebBrowser(this);
        }

        public EdgeDevWebBrowserFactory(TestSuiteRunner runner) : base(runner)
        {
        }
    }
}
