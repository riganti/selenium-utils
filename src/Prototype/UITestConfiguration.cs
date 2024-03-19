using System.Collections.Frozen;
using OpenQA.Selenium;

namespace Riganti.Selenium.Prototype;

public record UITestConfiguration
{
    // TODO: Add WebDriver path somewhere

    public FrozenDictionary<WebDriverKind, WebDriverConfiguration> WebDrivers { get; }

    public static UITestConfiguration CreateDefault()
    {
        return new();
    }
}
