using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core;

namespace Riganti.Selenium.Sandbox;

public class Program : SeleniumTest
{
    public static void DefinitelyNotMain()
    {
        Trace.WriteLine("test trace");
        Debug.WriteLine("test debug");
        Console.WriteLine("test console");
        foreach(var l in Trace.Listeners) {
            Console.WriteLine(l);
        }
        return;
        
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
