using Xunit;

namespace Riganti.Selenium.Prototype.Tests;

public class SampleTests
{
    [Fact]
    public void Simple_WithManualTestContext()
    {
        var pool = new UITestContextPool(UITestConfiguration.CreateDefault());
        var ctx = pool.Obtain(UITestContextOptions.CreateDefault());
        ctx.WebDriver.Navigate().GoToUrl("https://example.com");
        pool.Return(ctx);
    }
}
