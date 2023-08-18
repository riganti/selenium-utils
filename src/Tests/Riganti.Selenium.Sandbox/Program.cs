using System;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core;

namespace Riganti.Selenium.Sandbox;

public class Program : SeleniumTest
{
    public static void Main()
    {
        var test = new Program();

        test.RunInAllBrowsers(browser =>
        {
            browser.NavigateToUrl("/test/CssStyles");

                browser.First("#hasStyles")
                    .CheckCssStyle("font-size", "8px")
                    .CheckCssStyle("width", "20px")
                    .CheckCssStyle("height", "20px");

                browser.First("#hasNotStyles")
                    .CheckCssStyle("margin-left", "8px");
        });
        
        test.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/test/FindElements");
                var a = browser.FindElements("div");
                var b = a.FindElements("p");
                var c = b.BrowserWrapper;
                var d = c.FindElements("div");
                var e = d.FindElements("p");
                e.ThrowIfSequenceEmpty();
            });
        
        Console.WriteLine("Exiting successfully...");
    }
}
