using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Riganti.Selenium.DotVVM;
namespace Riganti.Selenium.DotVVM3.Samples.Tests
{
    public class DotvvmPageLoadTests : AppSeleniumTest
    {
        public DotvvmPageLoadTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void InitFunctionTests()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.WaitUntilDotvvmInited();
            });
        }
    }
}
