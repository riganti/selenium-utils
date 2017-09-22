using System;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.xUnitIntegration;
using Xunit.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.LambdaApi.Tests
{
    public abstract class AppSeleniumTest : SeleniumTest 
    {
        public virtual void RunInAllBrowsers(Action<IBrowserWrapper> wrapper)
        {
            throw new NotImplementedException();
        }

        protected AppSeleniumTest(ITestOutputHelper output) : base(output) {}
    }
}
