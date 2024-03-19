using OpenQA.Selenium;

namespace Riganti.Selenium.Prototype;

public record WebDriverConfiguration : IWebDriverConfiguration
{
    public string? BrowserName { get; init; }
    public string? BrowserVersion { get; init; }
    public string? PlatformName { get; init; }
    public bool? AcceptInsecureCertificates { get; init; }
    public bool? UseWebSocketUrl { get; init; }
    public UnhandledPromptBehavior UnhandledPromptBehavior { get; init; }
}
