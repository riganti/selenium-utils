namespace Riganti.Selenium.Prototype;

public record UITestContextOptions : IUITestContextOptions
{
    public static UITestContextOptions CreateDefault()
    {
        return new();
    }
}
