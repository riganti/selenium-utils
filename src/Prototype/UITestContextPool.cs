namespace Riganti.Selenium.Prototype;

public class UITestContextPool : IUITestContextPool
{
    public UITestContextPool(UITestConfiguration configuration)
    {
        Configuration = configuration;
    }

    public UITestConfiguration Configuration { get; }

    public UITestContext Obtain(IUITestContextOptions options)
    {
        throw new System.NotImplementedException();
    }

    public void Return(UITestContext ctx)
    {
        throw new System.NotImplementedException();
    }
}
