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
    }
}
