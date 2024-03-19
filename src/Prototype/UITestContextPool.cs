using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;

namespace Riganti.Selenium.Prototype;

public class UITestContextPool : IUITestContextPool
{
    // TODO: Pass driver path from UITestConfiguration
    public readonly ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
    public readonly HashSet<WebDriver> webDrivers = new();

    public UITestContextPool(UITestConfiguration configuration)
    {
        Configuration = configuration;
    }

    public UITestConfiguration Configuration { get; }

    public void Dispose()
    {
        foreach(var webDriver in webDrivers)
        {
            webDriver.Dispose();
        }
    }

    public UITestContext Obtain(IUITestContextOptions options)
    {
        var driver = new ChromeDriver(chromeDriverService, new ChromeOptions());
        return new UITestContext(driver);
    }

    public void Return(UITestContext ctx)
    {
        if(ctx.WebDriver is not null)
        {
            ctx.WebDriver.Dispose();
            webDrivers.Remove(ctx.WebDriver);
        }
    }
}
