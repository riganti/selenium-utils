using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Riganti.Selenium.Core.Samples.AssertApi.Tests
{

    public class HasFocusTests : AppSeleniumTest
    {
        public HasFocusTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void HasFocusTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/Focus");
               var elms = new List<IElementWrapper>();
                elms.AddRange(browser.FindElements("a"));
                elms.AddRange(browser.FindElements("input"));
                elms.AddRange(browser.FindElements("textarea"));

                foreach (var elm in elms)
                {
                    elm.SetFocus();
                    foreach (var item in elms)
                    {
                        if (item != elm)
                        {
                            Assert.False(item.HasFocus(), $"Element: {item.GetTagName()} : {item.GetAttribute("type")} has focus and should not.");
                        }
                        else
                        {
                            Assert.True(item.HasFocus(), $"Element: {item.GetTagName()} : {item.GetAttribute("type")} should have focus.");
                        }
                    }
                }
            });
        }
    }
}
