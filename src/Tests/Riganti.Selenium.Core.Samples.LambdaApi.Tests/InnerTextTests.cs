using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Riganti.Selenium.Core.Samples.LambdaApi.Tests
{
    public class InnerTextTests : AppSeleniumTest
    {
        public InnerTextTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void TextTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("text.aspx");

                ////A
                //browser.First("#button")
                //    .Check()
                //    .InnerText(s => s.Equals("Text"))
                //    .Tag(s => s.IsIn("a", "button"));

                //    //browser.First("#button").Check().InnerText(s => s.Contains("text"));
                //    //browser.First("#input").Check().Tag(s => s.Contains("input"));
                //    //browser.First("#area").Check().InnerText(s => s.Contains("text"));
                //    //browser.First("#area").Check().Value(s => s.Contains("text", StringComparison.OrdinalIgnoreCase));
                //    //browser.First("#area").Check().Value(s => s.StartsWith("text", StringComparison.OrdinalIgnoreCase));
                //    //browser.First("#area").Value(s => s.EndsWith("text", StringComparison.OrdinalIgnoreCase));
                //    //browser.First("#area").Check().Value(s => s.Count("text", StringComparison.OrdinalIgnoreCase));
                //    //browser.First("#area").Check().Value(s => { s.Trim = true; s.StartsWith("text"); });
            });
        }



    }
}
