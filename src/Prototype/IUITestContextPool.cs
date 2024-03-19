namespace Riganti.Selenium.Prototype;

public interface IUITestContextPool
{
    UITestContext Obtain(IUITestContextOptions options);

    void Return(UITestContext ctx);
}
