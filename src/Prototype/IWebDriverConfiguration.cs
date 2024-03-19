using OpenQA.Selenium;

namespace Riganti.Selenium.Prototype;

/// <summary>
/// A merger of all of Selenium's <see cref="DriverOptions"/>
/// </summary>
public interface IWebDriverConfiguration
{
    /// <summary>
    /// See <see cref="DriverOptions.BrowserName"/>.
    /// </summary>
    string? BrowserName { get; }

    /// <summary>
    /// See <see cref="DriverOptions.BrowserVersion"/>.
    /// </summary>
    string? BrowserVersion { get; }

    /// <summary>
    /// See <see cref="DriverOptions.PlatformName"/>.
    /// </summary>
    string? PlatformName { get; }

    /// <summary>
    /// See <see cref="DriverOptions.AcceptInsecureCertificates"/>.
    /// </summary>
    bool? AcceptInsecureCertificates { get; }

    /// <summary>
    /// See <see cref="DriverOptions.UseWebSocketUrl"/>
    /// </summary>
    bool? UseWebSocketUrl { get; }

    /// <summary>
    /// See <see cref="DriverOptions.UnhandledPromptBehavior"/>
    /// </summary>
    UnhandledPromptBehavior UnhandledPromptBehavior { get; }
    
    // TODO: Add other props.
}
