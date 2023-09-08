using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Runners;

namespace Riganti.Selenium.Sandbox;

public class Program : SeleniumTest
{
    public static int Main()
    {
        var runner = AssemblyRunner.WithoutAppDomain(typeof(Program).Assembly.Location);
        
        runner.OnDiagnosticMessage = message =>
        {
            Console.WriteLine(message);
        };

        var success = true;

        runner.OnTestStarting = info =>
        {
            Console.WriteLine($"[xUnit] {info.TypeName}.{info.MethodName}: starting");
        };

        runner.OnTestFailed = info =>
        {
            success = false;
            Console.WriteLine($"[xUnit] {info.TypeName}.{info.MethodName}: failed");
        };
        
        runner.OnTestPassed = info =>
        {
            Console.WriteLine($"[xUnit] {info.TypeName}.{info.MethodName}: passed");
        };

        runner.Start(parallel: false);

        var waitOne = new System.Threading.ManualResetEvent(false);
        runner.OnExecutionComplete = info =>
        {
            waitOne.Set();
        };

        waitOne.WaitOne();
        
        Console.WriteLine(success ? "Success" : "Failure");
        return success ? 0 : 1;
    }
    
    private static void RunInlineSamplesTests()
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
