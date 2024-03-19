using System;

namespace Riganti.Selenium.Prototype;

public interface IUITestContextPool : IDisposable
{
    UITestContext Obtain(IUITestContextOptions options);

    void Return(UITestContext ctx);
}
