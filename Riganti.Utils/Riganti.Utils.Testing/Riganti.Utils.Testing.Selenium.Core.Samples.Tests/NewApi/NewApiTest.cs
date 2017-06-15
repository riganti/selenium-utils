using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.Tests.NewApi
{
    [TestClass]
    public class NewApiTest : SeleniumTest
    {
        [TestMethod]
        public void TextTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("text.aspx");

                //A
                browser.First("#button")
                        .Check()
                            .InnerText(s => s.Equals("text"))
                            .Tag(s => s.IsIn("a", "b"));

                browser.First("#button").Check().InnerText(s => s.Contains("text"));
                browser.First("#input").Check().Tag(s => s.Contains("input"));
                browser.First("#area").Check().InnerText(s => s.Contains("text"));
                browser.First("#area").Check().Value(s => s.Contains("text", StringComparison.OrdinalIgnoreCase));
                browser.First("#area").Check().Value(s => s.StartsWith("text", StringComparison.OrdinalIgnoreCase));
                browser.First("#area").Check().Value(s => s.EndsWith("text", StringComparison.OrdinalIgnoreCase));
                browser.First("#area").Check().Value(s => s.Count("text", StringComparison.OrdinalIgnoreCase));
                browser.First("#area").Check().Value(s => { s.Trim = true; s.StartsWith("text"); });
            });
        }
    }
}